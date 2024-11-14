using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))] // temp for testing checkpoints
public class CheckPoints : MonoBehaviour {

    //static List<CheckPoints> CHECKPOINTS = new List<CheckPoints>();

    static public bool checkptCross = false;

    //[SerializeField] private GameObject lastCheckpoint = null;

    /*void OnPassThrough(Collider other) {
            GameObject Ckpt = other.GetComponent<CheckPoints>();

            if (Ckpt != null) {
                Checkpoint.checkptCross = true;

                Material mat = GetComponent<Renderer>();
                Color c = mat.color;
                c.a = 0.75f;
                mat.color = c;
            }
    }*/
}
