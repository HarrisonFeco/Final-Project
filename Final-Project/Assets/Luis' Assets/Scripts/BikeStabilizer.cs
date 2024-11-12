using UnityEngine;

public class BikeStabilizer : MonoBehaviour
{
  public float tiltAngle = 40f; // maximum tilt angle when turning
  public float tiltSpeed = 2f;  // how quickly the bike tilts
  private float currentTilt = 0f;

void Update()
{
    float turn = 0f;

    // Check if A or D keys are pressed
    if (Input.GetKey(KeyCode.A))
    {
        turn = 1f; // Tilt left
    }
    else if (Input.GetKey(KeyCode.D))
    {
        turn = -1f;  // Tilt right
    }

    // Target tilt angle based on turning input
    float targetTilt = turn * tiltAngle;

    // Smoothly adjust the current tilt towards the target
    currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

    // Apply the tilt rotation only on the local rotation
    transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, currentTilt);
}
}