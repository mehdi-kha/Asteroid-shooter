using System.Collections.Generic;
using UniRx;

namespace Modules.Enemies
{
    public interface IEnemiesModel
    {
        IReactiveCollection<IEnemy> VisibleEnemies { get; }
        ICollection<IEnemy> AvailableEnemies { get; }
    }
}