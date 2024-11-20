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

    [Header("Suspension")]
    public Transform suspensionObject; // The suspension GameObject to rotate
    public float maxSuspensionAngle = 22f;


    [Header("Bike Settings")]
    public float motorTorque = 300f;
    public float brakeForce = 800f;
    public float maxSteerAngle = 30f;
    //public float uprightForce = 50f;

    [Header("Terrain Handling")]
    public float slopeDownForce = 50f; // Force to keep wheels grounded
    public float maxSlopeAngle = 45f; // Maximum slope angle

    private float accelerationInput;
    private float brakeInput;
    private float steerInput;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Lower center of mass for stability
    }

    void FixedUpdate()
    {
        GetInput();
        HandleMotorAndSteeringOnSlopes();
        ApplyDownwardForce();
        AlignToSlope();
        StabilizeRollOnHills();
        DampenSidewaysMotion();
        UpdateWheelPositions();
        
    }

    /// <summary>
    /// Reads user input for acceleration, braking, and steering.
    /// </summary>
    private void GetInput()
    {
        accelerationInput = Input.GetKey(KeyCode.W) ? 1 : 0;
        brakeInput = Input.GetKey(KeyCode.S) ? 1 : 0;

        steerInput = Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D) ? 1 : 0);
    }

    /// <summary>
    /// Handles motor torque and steering behavior, reducing effectiveness on steep slopes.
    /// </summary>
    private void HandleMotorAndSteeringOnSlopes()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            // Adjust motor torque and steering on slopes
            float slopeFactor = Mathf.Clamp01((maxSlopeAngle - slopeAngle) / maxSlopeAngle);
            rearWheel.motorTorque = accelerationInput * motorTorque * slopeFactor;

            float steeringReduction = Mathf.Lerp(1f, 0.5f, slopeAngle / maxSlopeAngle);
            frontWheel.steerAngle = steerInput * maxSteerAngle * steeringReduction;
        }
        else
        {
            // Default behavior on flat ground
            rearWheel.motorTorque = accelerationInput * motorTorque;
            frontWheel.steerAngle = steerInput * maxSteerAngle;
        }
    }

    /// <summary>
    /// Applies forces to keep the bike grounded and counteract sliding.
    /// </summary>
    private void ApplyDownwardForce()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Vector3 groundNormal = hit.normal;
            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

            if (slopeAngle < maxSlopeAngle)
            {
                // Downward force
                rb.AddForce(-groundNormal * slopeDownForce, ForceMode.Acceleration);

                // Lateral stabilization
                Vector3 lateralForce = Vector3.Cross(groundNormal, transform.forward);
                rb.AddForce(-lateralForce * slopeDownForce * 0.5f, ForceMode.Acceleration);
            }
        }
    }

    /// <summary>
    /// Stabilizes the bike's roll (Z-axis) to prevent tipping over on slopes.
    /// </summary>
    private void StabilizeRollOnHills()
    {
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);
        if (tiltAngle > 5f)
        {
            Vector3 correctiveTorque = Vector3.Cross(transform.up, Vector3.up);
            rb.AddTorque(correctiveTorque * 1.5f, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Aligns the bike's forward direction to the slope's surface.
    /// </summary>
    private void AlignToSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Vector3 slopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, transform.forward), hit.normal).normalized;

            Vector3 targetForward = Vector3.Lerp(transform.forward, slopeDirection, Time.deltaTime * 2f);
            Quaternion targetRotation = Quaternion.LookRotation(targetForward, hit.normal);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 5f));
        }
    }

    /// <summary>
    /// Reduces lateral velocity to prevent sliding.
    /// </summary>
    private void DampenSidewaysMotion()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x *= 0.8f; // Dampen sideways velocity
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    /// <summary>
    /// Updates the visual representation of the wheels to match the physics.
    /// </summary>
    private void UpdateWheelPositions()
    {
        UpdateWheelTransform(frontWheel, frontWheelTransform);
        UpdateRearWheelTransform(rearWheel, rearWheelTransform);
    }

    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void UpdateRearWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;

        

        

    }

    

}
