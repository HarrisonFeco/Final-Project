using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The dirt bike (set in the Inspector)
    public Vector3 offset = new Vector3(0, 0.5f, -2); // Offset for the camera
    public float smoothSpeed = 0.125f; // Adjust for smoothness
    public float angleOffset = 10f; // Additional angle adjustment

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position with target's rotation
            Vector3 desiredPosition = target.position + target.rotation * offset;

            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Adjust the camera to look at the bike with an angle offset
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion angleAdjustedRotation = Quaternion.Euler(targetRotation.eulerAngles.x + angleOffset, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAdjustedRotation, smoothSpeed);
        }
    }
}