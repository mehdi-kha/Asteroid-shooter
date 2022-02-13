using System.Collections.Generic;
using UniRx;

namespace Modules.Enemies
{
    public interface IEnemiesModel
    {
        // TODO maybe a hashtable can be more efficient
        IReactiveCollection<IEnemy> VisibleEnemies { get; }
        IList<IEnemy> AvailableEnemies { get; }
    }
}