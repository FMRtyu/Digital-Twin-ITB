using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nickNameFollowCamera : MonoBehaviour
{
    private Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject != null)
        {
            cameraTransform = cameraObject.transform;
        }
        else
        {
            Debug.LogError("Camera not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTransform != null)
        {
            Vector3 lookAtPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
            transform.LookAt(lookAtPosition);
        }
        /*Camera camera = (Camera)FindObjectOfType(typeof(Camera));

        if (camera)
        {
            Vector3 lookAtPosition = new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z);
            transform.LookAt(lookAtPosition);
            transform.LookAt(camera.gameObject.transform);
        }*/
    }
}
