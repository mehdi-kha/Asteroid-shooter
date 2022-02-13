using System.Windows.Input;
using UniRx;
using UnityEngine;

namespace Modules.Enemies
{
    public interface IEnemy
    {
        Sprite Sprite { get; set; }
        int Number { get; set; }
        
        float Speed { get; set; }
        
        /// <summary>
        ///     The vertical world coordinate below which the enemy has won.
        /// </summary>
        float BottomWorldPosition { get; set; }

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
    }
}