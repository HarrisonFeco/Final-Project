using UnityEngine;

public class CameraFollow : MonoBehaviour
{
<<<<<<< HEAD
    public Transform target; 
    public Vector3 offset = new Vector3(0, 0.5f, -2); 
    public float smoothSpeed = 0.125f; 
    public float angleOffset = 10f; 
=======
    public Transform target; // Reference to the bike's Transform
    public Vector3 offset = new Vector3(0, 0.5f, 0); // Offset from the target for first-person view
    public float smoothSpeed = 0.125f; // Adjust for smoothness
    public float angleOffset = 10f; // Angle adjustment in degrees for up/down view
>>>>>>> main

    void LateUpdate()
    {
        if (target != null)
        {
<<<<<<< HEAD
            Vector3 desiredPosition = target.position + target.rotation * offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            Quaternion angleAdjustedRotation = Quaternion.Euler(targetRotation.eulerAngles.x + angleOffset, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, angleAdjustedRotation, smoothSpeed);
=======
            // Calculate the desired position directly in front of the bike
            Vector3 desiredPosition = target.position + target.rotation * offset;

            // Smoothly interpolate to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Align the camera's rotation with the bike's rotation, adding angle adjustment
            Quaternion targetRotation = target.rotation;

            // Apply pitch adjustment to the rotation
            Quaternion adjustedRotation = targetRotation * Quaternion.Euler(angleOffset, 0f, 0f);

            // Smoothly interpolate the camera's rotation to the adjusted rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, adjustedRotation, smoothSpeed);
>>>>>>> main
        }
    }
}