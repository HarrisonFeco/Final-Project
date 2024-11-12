using UnityEngine;

public class BikeStabilizer : MonoBehaviour
{
    public float stabilizationForce = 5f; // Force to keep the bike upright when stopped
  public float tiltAngle = 15f; // maximum tilt angle when turning
public float tiltSpeed = 2f;  // how quickly the bike tilts
private float currentTilt = 0f;

void Update()
{
    float turn = Input.GetAxis("Horizontal"); // adjust based on input method

    // Target tilt angle based on turning input
    float targetTilt = turn * tiltAngle;

    // Smoothly adjust the current tilt towards the target
    currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

    // Apply the tilt rotation only on the local rotation
    transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, currentTilt);
}
}