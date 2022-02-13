using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Modules.Enemies;
using Modules.Inputs;
using Modules.UI;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Toolkit;

public class EnemiesManager : MonoBehaviour
{
    [Inject] private IInputManager inputManager;
    [Inject] private IEnemiesModel enemiesModel;
    [Inject] private IUIModel uiModel;
    
    [SerializeField] private Sprite[] enemiesBackgrounds;

    [SerializeField] private GameObject emptyEnemy;

    [SerializeField] private float rate = 0.99f;

    [SerializeField] private int initialInterval = 1000;

    [SerializeField] private float enemySpeed = 2;
    
    [SerializeField]
    private bool collectionChecks = true;
    [SerializeField]
    public int defaultPoolSize = 100;
    
    System.Random random = new System.Random();

    private Vector2 topLeftPosition;
    private Vector2 topRightPosition;

    private CancellationTokenSource tokenSource;

    void Start()
    {
        this.inputManager.Shoot.Subscribe(OnShoot);
        this.enemiesModel.VisibleEnemies.ObserveAdd().Subscribe(e => this.OnVisibleEnemyAdded(e.Value));
        this.enemiesModel.VisibleEnemies.ObserveRemove().Subscribe(e => this.OnVisibleEnemyRemoved(e.Value));

        this.topLeftPosition = this.uiModel.TopLeftWorldPosition();
        this.topRightPosition = this.uiModel.TopRightWorldPosition();
        
        // Fill in the pool
        if (this.enemiesModel.AvailableEnemies == null)
        {
            this.enemiesModel.AvailableEnemies = new UnityEngine.Pool.ObjectPool<IEnemy>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, defaultPoolSize);
        }

        // Start spawning
        tokenSource = new System.Threading.CancellationTokenSource();
        tokenSource.Token.ThrowIfCancellationRequested();
        this.SpawnEnemiesPeriodically(initialInterval, this.rate, tokenSource);
    }

    private void OnShoot(int number)
    {
        // Find the first enemy in the queue matching the number
        var matchedEnemy = this.enemiesModel.VisibleEnemies.FirstOrDefault(enemy => enemy.Number == number);
        if (matchedEnemy != null)
        {
            this.enemiesModel.AvailableEnemies.Release(matchedEnemy);
            this.enemiesModel.VisibleEnemies.Remove(matchedEnemy);
        }
    }

    private IEnemy InstantiateRandomEnemy()
    {
        var enemySprite = enemiesBackgrounds[random.Next(enemiesBackgrounds.Length)];
        var enemyValue = random.Next(10);
        var enemyGameObject = Instantiate(emptyEnemy);
        var enemy = enemyGameObject.GetComponent<IEnemy>();
        enemy.Sprite = enemySprite;
        enemy.Number = enemyValue;
        enemy.Speed = 0;
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
        enemy.Speed = this.enemySpeed;
    }

    private void OnVisibleEnemyRemoved(IEnemy enemy)
    {
        // Move it back to the top of the screen, and set its speed to 0
        this.PositionEnemyRandomlyAboveScreen(enemy);
        enemy.Speed = 0;
    }
    
    IEnemy CreatePooledItem()
    { 
        return this.InstantiateRandomEnemy();
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
        this.tokenSource.Cancel();
    }
}
