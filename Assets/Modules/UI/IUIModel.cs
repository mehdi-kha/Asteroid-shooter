using UnityEngine;

namespace Modules.UI
{
    public interface IUIModel
    {
        Vector2 TopLeftWorldPosition();
        Vector2 TopRightWorldPosition();
    }
}