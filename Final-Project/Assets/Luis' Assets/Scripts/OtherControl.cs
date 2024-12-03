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
        rb.centerOfMass = new Vector3(0, -0.2f, 0); 
    }

    void Update()
    {
        Inputs();
        Drive();
        SteerCar();

        UpdateWheelPos(wheel_f_r, frontWheel); //help keep the wheels together
        UpdateWheelPos(wheel_b, backWheel);
    }

    void Inputs()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void Drive()
    {
        wheel_b.motorTorque = v * _motorForce; //torque
    }

     void SteerCar()
    {
        steerAngl = _steerAngle * h;
        wheel_f_r.steerAngle = steerAngl;

        steering.transform.localRotation = Quaternion.Euler(-30, steerAngl, 0); //steer angle
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