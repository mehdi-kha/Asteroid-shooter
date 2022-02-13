using Modules.Enemies;
using Modules.Inputs;
using Modules.UI;
using UnityEngine;
using Zenject;
using UniRx;

public class EnemiesManager : MonoBehaviour
{
    [Inject] private IInputManager inputManager;
    [Inject] private IEnemiesModel enemiesModel;
    [Inject] private IUIModel uiModel;
    
    [SerializeField] private Sprite[] enemiesBackgrounds;

    [SerializeField] private int enemiesPoolSize = 100;

    [SerializeField] private GameObject emptyEnemy;
    
    System.Random random = new System.Random();

    private Vector2 topLeftPosition;
    private Vector2 topRightPosition;

    void Start()
    {
        this.inputManager.Shoot.Subscribe(OnShoot);

        this.topLeftPosition = this.uiModel.TopLeftWorldPosition();
        this.topRightPosition = this.uiModel.TopRightWorldPosition();
        
        // Fill in the pool
        for (int i = 0; i < this.enemiesPoolSize; i++)
        {
            var enemy = this.InstantiateRandomEnemy();
            this.enemiesModel.AvailableEnemies.Add(enemy);
        }
    }

    private void OnShoot(int number)
    {
        Debug.Log(number);
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
}
