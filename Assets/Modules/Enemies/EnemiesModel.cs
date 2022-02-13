using System.Collections.Generic;
using UniRx;

namespace Modules.Enemies
{
    public class EnemiesModel : IEnemiesModel
    {
        // TODO: Find some reactive queue instead of a collection
        public IReactiveCollection<IEnemy> VisibleEnemies { get; }
        
        public ICollection<IEnemy> AvailableEnemies { get; }

        public EnemiesModel()
        {
            this.VisibleEnemies = new ReactiveCollection<IEnemy>();
            this.AvailableEnemies = new List<IEnemy>();
        }
    }
}