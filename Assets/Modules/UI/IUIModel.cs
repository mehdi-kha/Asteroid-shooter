using UnityEngine;

namespace Modules.UI
{
    public interface IUIModel
    {
        Vector2 TopLeftWorldPosition { get; }
        Vector2 TopRightWorldPosition { get; }
        Vector2 BottomLeftWorldPosition { get; }
    }
}