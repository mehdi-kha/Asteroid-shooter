using System.Collections.Generic;
using UniRx;
using UnityEngine.Pool;

namespace Modules.Enemies
{
    public interface IEnemiesModel
    {
        // TODO maybe a hashtable can be more efficient
        IReactiveCollection<IEnemy> VisibleEnemies { get; }
        IObjectPool<IEnemy> AvailableEnemies { get; set; }
    }
}