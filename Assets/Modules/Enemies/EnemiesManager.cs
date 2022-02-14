using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Modules.Enemies.EnemyType;
using Modules.Game;
using Modules.Helper;
using Modules.Inputs;
using Modules.UI;
using UnityEngine;
using Zenject;
using UniRx;

namespace Modules.Enemies
{
    /// <summary>
    ///     Handles enemies spawning and shooting.
    /// </summary>
    public class EnemiesManager : MonoBehaviour
    {
        private IInputManager inputManager;
        private IEnemiesModel enemiesModel;
        private IUIModel uiModel;
        private IGameModel gameModel;

        [SerializeField] private GameObject emptyEnemy;

        [SerializeField] private float rate = 0.99f;

        [SerializeField] private int initialInterval = 1000;

        [SerializeField] private EnemyTypeScriptableObject[] enemyTypes;

        [SerializeField]
        private bool collectionChecks = true;
        [SerializeField]
        public int defaultPoolSize = 100;
        
        System.Random random = new System.Random();

        private Vector2 topLeftPosition;
        private Vector2 topRightPosition;
        private Vector2 bottomLeftPosition;

        private CancellationTokenSource tokenSource;

        private int currentInterval;

        [Inject]
        public void Initialize(IInputManager inputManager, IEnemiesModel enemiesModel, IUIModel uiModel,
            IGameModel gameModel)
        {
            this.inputManager = inputManager;
            this.enemiesModel = enemiesModel;
            this.uiModel = uiModel;
            this.gameModel = gameModel;
            
            this.inputManager.Shoot.Subscribe(OnShoot);
            this.enemiesModel.VisibleEnemies.ObserveAdd().Subscribe(e => this.OnVisibleEnemyAdded(e.Value));
            this.enemiesModel.VisibleEnemies.ObserveRemove().Subscribe(e => this.OnVisibleEnemyRemoved(e.Value));
            this.gameModel.GameStatus.Pairwise().Subscribe(OnGameStatusChanged);

            this.topLeftPosition = this.uiModel.TopLeftWorldPosition;
            this.topRightPosition = this.uiModel.TopRightWorldPosition;
            this.bottomLeftPosition = this.uiModel.BottomLeftWorldPosition;
        }

        private void InitializeEnemies()
        {
            // Reinitialize the current score
            this.gameModel.Score.Value = 0;
            
            // Fill in the pool
            if (this.enemiesModel.AvailableEnemies != null)
            {
                this.enemiesModel.AvailableEnemies.Clear();
                this.enemiesModel.AvailableEnemies = null;
            }
            this.enemiesModel.AvailableEnemies = new ObjectPoolWithQueue<IEnemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultPoolSize);

            // Start spawning
            if (this.enemiesModel.VisibleEnemies.Count != 0)
            {
                foreach (var visibleEnemy in this.enemiesModel.VisibleEnemies)
                {
                    visibleEnemy.Destroy();
                }
                this.enemiesModel.VisibleEnemies.Clear();
            }
            this.tokenSource = new System.Threading.CancellationTokenSource();
            this.tokenSource.Token.ThrowIfCancellationRequested();
            this.SpawnEnemiesPeriodically(initialInterval, this.rate, tokenSource);
        }

        private void OnGameStatusChanged(Pair<GameStatus> status)
        {
            switch (status.Current)
            {
                case GameStatus.GameOver:
                case GameStatus.Paused:
                    // Stop spawning periodically
                    this.tokenSource.Cancel();
                    foreach (var enemy in this.enemiesModel.VisibleEnemies)
                    {
                        // Stop all enemies still falling
                        enemy.Stop();
                    }

                    break;
                case GameStatus.Playing:
                    if (status.Previous != GameStatus.Paused)
                    {
                        this.InitializeEnemies();
                        return;
                    }
                    foreach (var enemy in this.enemiesModel.VisibleEnemies)
                    {
                        // Unpause
                        enemy.Move();
                    }
                    // Spawn periodically again, based on the last known spawning rate
                    this.tokenSource = new CancellationTokenSource();
                    tokenSource.Token.ThrowIfCancellationRequested();
                    this.SpawnEnemiesPeriodically(this.currentInterval, this.rate, this.tokenSource);

                    break;
            }
        }

        private void OnShoot(int number)
        {
            // Find the first enemy in the queue matching the number
            var matchedEnemy = this.enemiesModel.VisibleEnemies.FirstOrDefault(enemy => enemy.Number == number);
            if (matchedEnemy == null)
            {
                return;
            }
            this.enemiesModel.AvailableEnemies.Release(matchedEnemy);
            this.enemiesModel.VisibleEnemies.Remove(matchedEnemy);
            this.gameModel.Score.Value += 1;
        }

        private IEnemy InstantiateRandomEnemy()
        {
            var enemyType = this.enemyTypes[random.Next(enemyTypes.Length)];
            var enemySprite = enemyType.Sprites[random.Next(enemyType.Sprites.Length)];
            var enemyValue = random.Next(10);
            var enemyGameObject = Instantiate(emptyEnemy);
            var enemy = enemyGameObject.GetComponent<IEnemy>();
            enemy.Sprite = enemySprite;
            enemy.Number = enemyValue;
            enemy.BottomWorldPosition = this.bottomLeftPosition.y;
            enemy.EnemyType = enemyType;
            this.PositionEnemyRandomlyAboveScreen(enemy);
            return enemy;
        }

        private void PositionEnemyRandomlyAboveScreen(IEnemy enemy)
        {
            var horizontalDistance = topRightPosition.x - topLeftPosition.x;
            var horizontalPosition = ((float)random.NextDouble() * horizontalDistance) - (horizontalDistance / 2);
            enemy.SetPosition(new Vector2(horizontalPosition, topLeftPosition.y + enemy.Sprite.bounds.extents.y));
        }

        private async void SpawnEnemiesPeriodically(int millisecondsBeforeNextSpawn, float rate, CancellationTokenSource tokenSource)
        {
            this.currentInterval = millisecondsBeforeNextSpawn;
            if (tokenSource.IsCancellationRequested)
                return;

            try
            {
                await Task.Delay(millisecondsBeforeNextSpawn, tokenSource.Token);
            }
            catch (TaskCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }

            this.SpawnEnemy();

            // Spawn the next one
            this.SpawnEnemiesPeriodically((int)(rate * millisecondsBeforeNextSpawn), rate, tokenSource);
        }

        private void SpawnEnemy()
        {
            var enemy = this.enemiesModel.AvailableEnemies.Get();
            this.enemiesModel.VisibleEnemies.Add(enemy);
        }

        private void OnVisibleEnemyAdded(IEnemy enemy)
        {
            enemy.Move();
            // Connect the enemy's victory with the overall game status
            enemy.BottomReached.Subscribe(OnEnemyReachBottom);
        }

        private void OnEnemyReachBottom(IEnemy enemy)
        {
            this.tokenSource?.Cancel();
            this.gameModel.GameStatus.Value = GameStatus.GameOver;
        }

        private void OnVisibleEnemyRemoved(IEnemy enemy)
        {
            // Move it back to the top of the screen, and set its speed to 0
            this.PositionEnemyRandomlyAboveScreen(enemy);
            enemy.Stop();
        }
        
        IEnemy CreatePooledItem()
        { 
            var enemy = this.InstantiateRandomEnemy();
            return enemy;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(IEnemy enemy)
        {
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(IEnemy enemy)
        {
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(IEnemy enemy)
        {
            enemy.Destroy();
        }

        private void OnApplicationQuit()
        {
            this.tokenSource?.Cancel();
        }
    }
}
