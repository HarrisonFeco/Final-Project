using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSync : MonoBehaviour
{
    public WheelCollider wheelCollider; // Assign in Inspector
    public Transform wheelMesh; // Wheel mesh to sync
    public GameObject bike;
    public GameObject wheel;

    
    private void Start()
    {
        wheelCollider = wheel.GetComponent<WheelCollider>();
    }
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 position;
        Quaternion rotation;

        // Get the current world position and rotation of the WheelCollider
        wheelCollider.GetWorldPose(out position, out rotation);

        // Apply the position and rotation to the visual wheel object (wheelMesh or wheel)
        wheel.transform.position = bike.transform.InverseTransformPoint(position);
        wheel.transform.rotation = rotation;
    }
}

