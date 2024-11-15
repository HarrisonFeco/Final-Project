using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheck : MonoBehaviour {
    //protected TimingLoop TL;

    void OnTriggerEnter(Collider other) {
        Debug.Log(gameObject.name + " Checkpoint passed by " + other.gameObject.name);

    }
}