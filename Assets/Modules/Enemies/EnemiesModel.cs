using UniRx;
using UnityEngine.Pool;

namespace Modules.Enemies
{
    public class EnemiesModel : IEnemiesModel
    {

        // TODO - improvement: A reactive queue would be better
        public IReactiveCollection<IEnemy> VisibleEnemies { get; }
        
        public IObjectPool<IEnemy> AvailableEnemies { get; set;  }

        public EnemiesModel()
        {
            this.VisibleEnemies = new ReactiveCollection<IEnemy>();
        }
    }
}