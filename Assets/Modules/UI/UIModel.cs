using System.Collections;
using System.Collections.Generic;
using Modules.UI;
using UnityEngine;

public class UIModel : MonoBehaviour, IUIModel
{
    [SerializeField] private Camera camera;

    public Vector2 TopLeftWorldPosition()
    {
        return camera.ScreenToWorldPoint(new Vector2(0, camera.pixelHeight));
    }
    
    public Vector2 TopRightWorldPosition()
    {
        return camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));
    }
}
