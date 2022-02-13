using Modules.Enemies;
using Modules.Inputs;
using UnityEngine;
using Zenject;
using UniRx;

public class EnemiesManager : MonoBehaviour
{
    [Inject] private IInputManager inputManager;
    [Inject] private IEnemiesModel enemiesModel;
    
    [SerializeField] private Sprite[] enemiesBackgrounds;

    [SerializeField] private int enemiesPoolSize = 100;

    [SerializeField] private GameObject emptyEnemy;
    
    System.Random random = new System.Random();

    void Start()
    {
        this.inputManager.Shoot.Subscribe(OnShoot);
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
        return enemy;
    }
}
