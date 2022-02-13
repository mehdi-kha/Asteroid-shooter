using System.Collections.Generic;
using UniRx;
using UnityEngine.Pool;

namespace Modules.Enemies
{
    public class EnemiesModel : IEnemiesModel
    {

        // TODO: Find some reactive queue instead of a collection
        public IReactiveCollection<IEnemy> VisibleEnemies { get; }
        
        public IObjectPool<IEnemy> AvailableEnemies { get; set;  }

        public EnemiesModel()
        {
            this.VisibleEnemies = new ReactiveCollection<IEnemy>();
        }
    }
}