using UnityEngine;

namespace Modules.Enemies
{
    public interface IEnemy
    {
        Sprite Sprite { get; set; }
        int Number { get; set; }
    }
}