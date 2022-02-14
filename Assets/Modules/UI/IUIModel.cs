using UnityEngine;

namespace Modules.UI
{
    /// <summary>
    ///     Holds information about the UI
    /// </summary>
    public interface IUIModel
    {
        /// <summary>
        ///     World position of the top left screen point.
        /// </summary>
        Vector2 TopLeftWorldPosition { get; }
        
        /// <summary>
        ///     World position of the top right screen point.
        /// </summary>
        Vector2 TopRightWorldPosition { get; }
        
        /// <summary>
        ///     World position of the bottom left screen point.
        /// </summary>
        Vector2 BottomLeftWorldPosition { get; }
    }
}