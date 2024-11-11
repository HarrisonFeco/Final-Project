using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // The dirt bike (set in the Inspector)
    public Vector3 offset = new Vector3(0, 0.5f, -2); // Updated offset for the camera
    public float smoothSpeed = 0.125f; // Adjust for smoothness

    void LateUpdate()
    {
        if (target != null)
        {
            // Desired position based on target position and offset
            Vector3 desiredPosition = target.position + offset;
            // Smoothly interpolate between current position and desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Make the camera look at the dirt bike
            transform.LookAt(target);
        }
    }
}