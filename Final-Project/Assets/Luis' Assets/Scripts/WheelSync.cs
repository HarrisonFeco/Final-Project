using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSync : MonoBehaviour
{
    public WheelCollider wheelCollider; // Assign in Inspector
    public Transform wheelMesh; // Wheel mesh to sync

    void Update()
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }
}

