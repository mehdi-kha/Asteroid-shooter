using System.Collections.Generic;
using UniRx;

namespace Modules.Enemies
{
    public interface IEnemiesModel
    {
        IReactiveCollection<IEnemy> VisibleEnemies { get; }
        IList<IEnemy> AvailableEnemies { get; }
    }
}