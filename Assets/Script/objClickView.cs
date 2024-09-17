using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objClickView : MonoBehaviour
{
    public void OnMouseDown()
    {
        CameraController.instance.followTransform = transform;
    }
}
