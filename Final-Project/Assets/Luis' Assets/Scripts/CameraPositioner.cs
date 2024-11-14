using UnityEngine;

public class CameraPositioner : MonoBehaviour
{
    public Transform targetObject; // Assign the object here
    public Vector3 offset = new Vector3(0, 0, -5); // Adjust as needed

    void Update()
    {
        if (targetObject != null)
        {
            // Move the camera to target position with offset
            transform.position = targetObject.position + offset;
            // Look at the target
            transform.LookAt(targetObject);
        }
    }
}
