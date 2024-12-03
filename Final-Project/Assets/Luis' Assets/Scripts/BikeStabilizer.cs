using UnityEngine;

public class BikeStabilizer : MonoBehaviour
{
  public float tiltAngle = 40f;
  public float tiltSpeed = 2f;  
  private float currentTilt = 0f;

void Update()
{
    float turn = 0f;
    if (Input.GetKey(KeyCode.A))
    {
        turn = 1f; // Tilt left
    }
    else if (Input.GetKey(KeyCode.D))
    {
        turn = -1f;  // Tilt right
    }

    float targetTilt = turn * tiltAngle;
    currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);
    transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, currentTilt);
}
}