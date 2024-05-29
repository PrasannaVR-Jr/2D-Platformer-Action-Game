using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform CameraTransform;
    public float yOffsetCamera=4;
    public float smoothValue = 3;
    public float xValueMin,xValueMax, yValueMin,yValueMax;
        
    void LateUpdate()
    {
        CameraTransform.position = Vector3.Lerp(CameraTransform.position, new Vector3(transform.position.x, transform.position.y + yOffsetCamera, -10), smoothValue*Time.deltaTime);
        CameraTransform.position = new Vector3(Mathf.Clamp(CameraTransform.position.x, xValueMin, xValueMax),CameraTransform.position.y, CameraTransform.position.z);
    }
}
