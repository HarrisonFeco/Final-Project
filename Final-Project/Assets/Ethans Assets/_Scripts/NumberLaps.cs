using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NumberLaps : MonoBehaviour
{   
    public void SetLaps(string input) {
        int numLaps = int.Parse(input);
        PlayerPrefs.SetInt("Laps", numLaps);
        Debug.Log("Number of laps set to " + numLaps);
    }
}
