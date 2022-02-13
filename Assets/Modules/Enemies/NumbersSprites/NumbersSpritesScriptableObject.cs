using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Enemies.NumbersSprites
{
    [CreateAssetMenu(fileName = "NumbersSprites", menuName = "ScriptableObjects/NumbersSpritesScriptableObject", order = 1)]
    public class NumbersSpritesScriptableObject : ScriptableObject
    {
         public Sprite[] NumbersSprites;
    }
}