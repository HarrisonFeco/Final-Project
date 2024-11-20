using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtBikeController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontWheel;
    public WheelCollider rearWheel;

    [Header("Wheel Transforms")]
    public Transform frontWheelTransform;
    public Transform rearWheelTransform;

    [Header("Bike Settings")]
    public float motorTorque = 300f;
    public float brakeForce = 800f;
    public float maxSteerAngle = 30f;
    public float uprightForce = 50f;

    [Header("Terrain Handling")]
    public float slopeDownForce = 50f; // Extra force to keep wheels grounded
    public float maxSlopeAngle = 45f; // Maximum angle the bike can handle

    private float accelerationInput = 0f;
    private float brakeInput = 0f;
    private float steerInput = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Lower center of mass for stability
        
    }

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        ApplyDownwardForce();
        UpdateWheelPositions();
        StabilizeBike();
        //AdjustGravity();
        ApplyLeanEffect();
    }

    private void GetInput()
    {
        accelerationInput = Input.GetKey(KeyCode.W) ? 1 : 0;
        brakeInput = Input.GetKey(KeyCode.S) ? 1 : 0;

        steerInput = 0;
        if (Input.GetKey(KeyCode.A))
        {
            steerInput = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            steerInput = 1;
        }
    }



private void ApplyLeanEffect()
{
    // Calculate lean angle based on steer input and speed
    float leanAngle = steerInput * Mathf.Clamp(rb.velocity.magnitude / 10f, 0.5f, 1f) * maxSteerAngle;

    // Apply leaning torque to roll into the turn
    Vector3 leanTorque = transform.forward * -leanAngle;
    rb.AddTorque(leanTorque, ForceMode.Acceleration);
}


    private void HandleMotor()
    {
        rearWheel.motorTorque = accelerationInput * motorTorque;
        rearWheel.brakeTorque = brakeInput * brakeForce;
    }

  

    private void HandleSteering()
{
    float speedFactor = rb.velocity.magnitude / 10f; // Normalize speed
    float steeringLimit = Mathf.Lerp(maxSteerAngle, maxSteerAngle * 0.5f, speedFactor); // Reduce steering sensitivity at high speed
    frontWheel.steerAngle = steerInput * steeringLimit;

    // Call Lean Effect to complement turning
    ApplyLeanEffect();
}


    private void ApplyDownwardForce()
    {
        // Calculate slope angle based on bike's forward direction and ground normal
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Vector3 groundNormal = hit.normal;
            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

            // Apply downward force to keep wheels grounded
            if (slopeAngle < maxSlopeAngle)
            {
                rb.AddForce(-groundNormal * slopeDownForce, ForceMode.Acceleration);
            }
        }
    }

    private void UpdateWheelPositions()
    {
        UpdateWheelTransform(frontWheel, frontWheelTransform);
        UpdateWheelTransform(rearWheel, rearWheelTransform);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    // private void StabilizeBike()
    // {
    //     // Vector3 correctiveTorque = Vector3.Cross(transform.up, Vector3.up);
    //     // rb.AddTorque(correctiveTorque * uprightForce, ForceMode.Acceleration);


    //     if (rb.angularVelocity.magnitude > 10f) // Adjust this threshold based on your needs
    //     {
    //         // Reduce angular velocity in the Y-axis to prevent spinning out
    //         Vector3 angularVelocity = rb.angularVelocity;
    //         angularVelocity.y = Mathf.Lerp(angularVelocity.y, 0f, 0.1f); // Gradually reduce the spin
    //         rb.angularVelocity = angularVelocity;
    //     }


    //     // Get the current angle between the bike's up vector and the world's up vector
    //     float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

    //     // If the tilt is small, we want to apply a smaller corrective force
    //     if (tiltAngle > 5f) // If the bike is leaning more than 5 degrees
    //     {
    //         // Calculate corrective torque to straighten the bike's up vector
    //         Vector3 correctiveTorque = Vector3.Cross(transform.up, Vector3.up);

    //         // Apply a stronger force to upright the bike gradually
    //         rb.AddTorque(correctiveTorque * uprightForce, ForceMode.Acceleration);
    //     }
    //     else
    //     {
    //         // Apply damping to smooth out any small wobble
    //         Vector3 angularVelocity = rb.angularVelocity;
    //         angularVelocity = Vector3.Lerp(angularVelocity, Vector3.zero, 0.1f);  // Dampen angular velocity
    //         rb.angularVelocity = angularVelocity;
    //     }
    // }

    private void StabilizeBike()
{
    // 1. Roll Stabilization (Z-Axis)
    float tiltAngle = Vector3.Angle(transform.up, Vector3.up); // Angle between bike's up and world up
    if (tiltAngle > 5f) // Tilt threshold
    {
        Vector3 correctiveTorque = Vector3.Cross(transform.up, Vector3.up); // Calculate corrective torque
        rb.AddTorque(correctiveTorque * uprightForce, ForceMode.Acceleration);
    }

    // 2. Yaw Damping (Y-Axis)
    if (rb.angularVelocity.magnitude > 10f) // Threshold for excessive rotation
    {
        Vector3 angularVelocity = rb.angularVelocity;
        angularVelocity.y = Mathf.Lerp(angularVelocity.y, 0f, Time.deltaTime * 2f); // Smoothly reduce yaw spin
        rb.angularVelocity = angularVelocity;
    }

    // 3. Smooth Small Wobbles
    if (tiltAngle <= 5f) // If tilt is small, dampen angular velocity for smoothness
    {
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, 0.1f);
    }
}

}
