using UnityEngine;

namespace Modules.Enemies
{
    public interface IEnemy
    {
        Sprite Sprite { get; set; }
        int Number { get; set; }
        
        float Speed { get; set; }

        /// <summary>
        ///     Set the enemy's position.
        /// </summary>
        /// <param name="wordPosition">The new world position.</param>
        public void SetPosition(Vector2 wordPosition);
    }
}