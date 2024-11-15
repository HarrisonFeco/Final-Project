
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class DirtBikeController : MonoBehaviour
{
    public static float moveForce = 100f;        // Force applied to move the dirt bike forward
    public float turnTorque = 1000f;       // Torque applied to turn the bike
    public float maxSpeed = 20f;          // Max speed the bike can reach
    public float brakeForce = 500f;       // Brake force to stop the bike
    public static float tiltAngle = 5f;         // How much the bike tilts during turning
    public float tiltSpeed = 3f;          // Speed of the tilt animation

    private Rigidbody rb;                 // Rigidbody component of the dirt bike
    private float currentSpeed;           // Current speed of the bike
    private PlayerInput playerInput;      // InputSystem

    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Get the Rigidbody at the start

        // Adjust the center of mass to be lower and towards the rear of the bike
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f); // Lower and rearward center of mass

        playerInput = GetComponent<PlayerInput>(); // Initializes playerInput
    }

    void Update()
    {
        // Get input for forward/backward movement (W/S)
        float moveInput = 0f;
        if (playerInput.actions["Throttle"].IsPressed())
        {
            moveInput = 1f; // Move forward
        }
        else if (playerInput.actions["Brake"].IsPressed()) 
        {
            moveInput = -1f; // Move backward (reverse)
        }

        // Apply forward force based on input, with a cap on max speed
        ApplyForwardMovement(moveInput);

        // Get input for turning (A/D)
        float turnInput = 0f;
        if (playerInput.actions["Lean Right"].IsPressed()) 
        {
            turnInput = 1f; // Turn right
        }
        else if (playerInput.actions["Lean Left"].IsPressed()) 
        {
            turnInput = -1f;  // Turn left
        }


        // Apply turning torque
        ApplyTurning(turnInput);

        // Apply brakes if no movement input (S or W) is pressed
        if (moveInput == 0f)
        {
            ApplyBrakes();
        }

        // Tilt the bike slightly based on the direction of movement
        ApplyTilt(turnInput);
    }

    void ApplyForwardMovement(float input)
    {
        if (input != 0f)
        {
            // Calculate the force to apply based on input and speed
            float force = input * moveForce;
            rb.AddForce(transform.forward * force, ForceMode.Force);

            // Limit the speed to maxSpeed
            currentSpeed = rb.velocity.magnitude;
            if (currentSpeed > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void ApplyTurning(float input)
    {
        if (input != 0f)
        {
            // Apply torque to turn the bike
            rb.AddTorque(Vector3.up * input * turnTorque * Time.deltaTime, ForceMode.Force);
        }
    }

    void ApplyBrakes()
    {
        // Apply brake force to stop the bike
        if (currentSpeed > 0f)
        {
            rb.drag = 2f;  // Simulate friction when brakes are applied
        }
        else
        {
            rb.drag = 0.5f;  // Low drag for smooth movement when no brakes are applied
        }
    }

    void ApplyTilt(float turnInput)
    {
        // Tilt the bike slightly based on the turning direction
        if (turnInput != 0f)
        {
            float tilt = -turnInput * tiltAngle;
            transform.Rotate(Vector3.forward, tilt * tiltSpeed * Time.deltaTime);
        }
    }

}