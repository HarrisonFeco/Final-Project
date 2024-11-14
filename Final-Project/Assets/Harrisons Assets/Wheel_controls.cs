using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_controls : MonoBehaviour
{
    public DirtBikeController DirtBikeController;
    public WheelCollider front_Col;
    public WheelCollider back_Col;
    public Transform front, rear;
    public float steeringAngle = DirtBikeController.tiltAngle;

    public float motorForce = DirtBikeController.moveForce;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        updateWheelPos(front_Col, front);
        updateWheelPos(back_Col, rear);

    }

    void updateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);
        t.position = pos;
        t.rotation = rot;
    }
}
