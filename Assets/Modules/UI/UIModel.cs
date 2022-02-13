using Modules.UI;
using UnityEngine;

public class UIModel : MonoBehaviour, IUIModel
{
    [SerializeField] private Camera camera;

    private Vector2? topLeftWorldPosition;
    private Vector2? topRightWorldPosition;
    private Vector2? bottomLeftWorldPosition;

    public Vector2 TopLeftWorldPosition
    {
        get
        {
            if (topLeftWorldPosition == null)
            {
                this.topLeftWorldPosition = camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight));
            }

            return this.topLeftWorldPosition.Value;
        }
    }

    public Vector2 TopRightWorldPosition
    {
        get
        {
            if (topRightWorldPosition == null)
            {
                this.topRightWorldPosition = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));
            }

            return this.topRightWorldPosition.Value;
        }
    }

    public Vector2 BottomLeftWorldPosition
    {
        get
        {
            if (bottomLeftWorldPosition == null)
            {
                this.bottomLeftWorldPosition = camera.ScreenToWorldPoint(new Vector2(0, 0));
            }

            return this.bottomLeftWorldPosition.Value;
        }
    }
}
