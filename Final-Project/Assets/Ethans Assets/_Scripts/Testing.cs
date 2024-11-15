using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Renderer))] // temp to verify hit
public class Testing : MonoBehaviour {
    //[Header("Inscribed")]
    //public GameObject CheckPoint;
   //public GameObject   bikePrefab;

    //static public bool chckPtHit = false;

    void OnTriggerEnter(Collider other) {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        Debug.Log("Checkpoint passed through! " + go.gameObject.name);

        /*if (collidedWith.CompareTag("Bike")) {
            //chckPtHit = true;

            Debug.Log("Checkpoint passed through!");

            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
        }*/
    }
}
