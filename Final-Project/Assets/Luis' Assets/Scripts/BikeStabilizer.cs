using UnityEngine;
using UnityEngine.InputSystem;

public class BikeStabilizer : MonoBehaviour
{
  public float tiltAngle = 40f;
  public float tiltSpeed = 2f;  
  private float currentTilt = 0f;
  private PlayerInput playerInput;

void Update()
{
    float turn = 0f;
    if (playerInput.actions["Lean Left"].IsPressed())
    {
        turn = 1f; // Tilt left
    }
    else if (playerInput.actions["Lean Right"].IsPressed())
    {
        turn = -1f;  // Tilt right
    }

    float targetTilt = turn * tiltAngle;
    currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
    transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, currentTilt);
}
}