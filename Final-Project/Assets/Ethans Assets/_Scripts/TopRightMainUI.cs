using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopRightMainUI : MonoBehaviour {
    [Header("Inscribed")]
    public TextMeshProUGUI     uitLap;
    //public TextMeshProUGUI     uitScore;
    public TextMeshProUGUI     uitTime;
    public GameObject[]          StartFinish;

    [Header("Dynamic")]
    private float   curTime;
    //private int     lapNum = 0;
    //private int     totalLaps = 12;
    //private int     score = 0;

    void Start() {
        //GameObject SF = Instantiate<GameObject>();
    }

    void Update() {
        curTime += Time.deltaTime;
        
        //uitLap.text = "Lap " + lapNum + " of " + totalLaps;
        //uitTime.text = "Current Time:\t" + Math.Round(curTime, 3);
    }

    string lastCheckPoint() {
        return "SF";
    }

    /*private void LapCounter() {
        if (lapNum == totalLaps) {
            Debug.Log("Race Has Finished!");
            return;
        }

        if (lastCheckPoint() == "SF") {
            Debug.Log("Racer hasn't completed proper checkpoints.");
            return;
        }
        lapNum++;
        return;
    }*/
}
