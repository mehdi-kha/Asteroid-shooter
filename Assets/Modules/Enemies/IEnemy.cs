using Modules.Enemies.EnemyType;
using UniRx;
using UnityEngine;

namespace Modules.Enemies
{
    /// <summary>
    ///     Represents an enemy
    /// </summary>
    public interface IEnemy
    {
        /// <summary>
        ///     The background sprite of the enemy.
        /// </summary>
        Sprite Sprite { get; set; }
        
        /// <summary>
        ///     The value associated to this enemy.
        /// </summary>
        int Number { get; set; }

        /// <summary>
        ///     The vertical world coordinate below which the enemy has won.
        /// </summary>
        float BottomWorldPosition { get; set; }
        
        /// <summary>
        ///     The species of this enemy.
        /// </summary>
        EnemyTypeScriptableObject EnemyType { get; set; }

        /// <summary>
        ///     Command triggered when the enemy wins.
        /// </summary>
        public IReactiveCommand<IEnemy> BottomReached { get; }

        /// <summary>
        ///     Set the enemy's position.
        /// </summary>
        /// <param name="wordPosition">The new world position.</param>
        public void SetPosition(Vector2 wordPosition);

        /// <summary>
        ///     Destroy this enemy's instance.
        /// </summary>
        public void Destroy();

        /// <summary>
        ///     Let the enemy move.
        /// </summary>
        public void Move();

        /// <summary>
        ///     Stop the enemy.
        /// </summary>
        public void Stop();
    }
}