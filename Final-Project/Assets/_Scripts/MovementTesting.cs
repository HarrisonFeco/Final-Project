using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTesting : MonoBehaviour
{
    static public MovementTesting S {get; private set; }

    [Header("Dynamic")]
    public float speed = 30;
    public float turningSpeed = 5;

    void Awake() {
        if (S == null) {
            S = this;
        }
        else {
            Debug.LogError("MovementTesting.Awake - Attempted to assign second object.");
        }
    }
    void Update() {
        float hAxis = Input.GetAxis("Lean"); // turning keys
        float vAxis = Input.GetAxis("Throttle"); // throttle keys

        Vector3 pos = transform.position;
        speed += vAxis;
        pos.x += hAxis * turningSpeed * Time.deltaTime;
        transform.position = pos;
    }
}
