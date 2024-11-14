using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherControl : MonoBehaviour
{
    public WheelCollider wheel_f_r;
    public WheelCollider wheel_b;
    public GameObject steering;

    public Transform frontWheel;
    public Transform backWheel;

    public float _steerAngle = 25f;
    public float _motorForce = 1500f;
    public float steerAngl;

    private float h, v;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.2f, 0);  // Adjusted center of mass for balance
    }

    void Update()
    {
        Inputs();
        Drive();
        SteerCar();

        UpdateWheelPos(wheel_f_r, frontWheel);
        UpdateWheelPos(wheel_b, backWheel);
    }

    void Inputs()
    {
        h = Input.GetAxis("Lean");
        v = Input.GetAxis("Throttle");
    }

    void Drive()
    {
        wheel_b.motorTorque = v * _motorForce;
    }

     void SteerCar()
    {
        steerAngl = _steerAngle * h;
        wheel_f_r.steerAngle = steerAngl;
    
        // Create a Quaternion rotation based on the steer angle for the Y-axis
        steering.transform.localRotation = Quaternion.Euler(0, steerAngl, 0);
    }

    void UpdateWheelPos(WheelCollider col, Transform t)
    {
        Vector3 pos = t.position;
        Quaternion rot = t.rotation;

        col.GetWorldPose(out pos, out rot);
        t.position = pos;
        t.rotation = rot;
        
    }
   

}