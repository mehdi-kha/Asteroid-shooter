using UnityEngine;

namespace Modules.Enemies.EnemyType
{
    /// <summary>
    ///     Gathers information about an enemy's species
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyTypeScriptableObject", order = 1)]
    public class EnemyTypeScriptableObject : ScriptableObject
    {
        public Sprite[] Sprites;
        public float Speed;
    }
}