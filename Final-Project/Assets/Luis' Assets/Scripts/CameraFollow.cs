using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 0.5f, -2); 
    public float smoothSpeed = 0.125f; 
    public float angleOffset = 10f; 

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + target.rotation * offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion angleAdjustedRotation = Quaternion.Euler(targetRotation.eulerAngles.x + angleOffset, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAdjustedRotation, smoothSpeed);
        }
    }
}