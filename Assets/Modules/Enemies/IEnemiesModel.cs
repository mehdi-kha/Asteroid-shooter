using UniRx;
using UnityEngine.Pool;

namespace Modules.Enemies
{
    /// <summary>
    ///     Stores the enemies in a pool.
    /// </summary>
    public interface IEnemiesModel
    {
        // TODO maybe a hashtable can be more efficient
        /// <summary>
        ///     Enemies that were spawned.
        /// </summary>
        IReactiveCollection<IEnemy> VisibleEnemies { get; }
        
        /// <summary>
        ///     Pool of enemies to be spawned.
        /// </summary>
        IObjectPool<IEnemy> AvailableEnemies { get; set; }
    }
}