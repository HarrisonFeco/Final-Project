using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingLoop : MonoBehaviour {
    [Header("Inscribed")]
    public GameObject[] CheckPoints;
    public GameObject   SFLine;
    public TextMeshProUGUI     UITxtLap;
    public TextMeshProUGUI     UITxtTime;
    public TextMeshProUGUI     UITxtBestTime;

    [Header("Dynamic")]
    [SerializeField] private GameObject curCheckPt;
    [SerializeField] private GameObject prevCheckPoint;
    [SerializeField] private int     curLap = 0; // current lap number
    [SerializeField] private float   startTime;  
    [SerializeField] private float   endTime;
    [SerializeField] private float   curTime;
    [SerializeField] private int     totalLaps = 1;
    [SerializeField] private int     numCPPassed = 0;
    [SerializeField] private float   bestTime;   // best lap time

    [Tooltip("Check box to reset best lap time.")]
    public bool resetBestLapTime = false;

    void Start() {
        if (PlayerPrefs.HasKey("BestTime")) {
            bestTime = PlayerPrefs.GetFloat("BestTime"); // loads best lap time
        }

        UITxtBestTime.text = "Best Time:   " + Math.Round(bestTime,3);
        UITxtLap.text = "Lap " + curLap + " of " + totalLaps;
    }

    void Update() {
        curTime += Time.deltaTime;

        UITxtTime.text = "Current Time:   " + Math.Round(curTime, 3);
    }

    void ChangeBestTime(float timeCheck) { // compared new time vs best lap time
        if (timeCheck < PlayerPrefs.GetFloat("BestTime")) { // if new time is better, save to PlayerPrefs
            PlayerPrefs.SetFloat("BestTime", timeCheck);
        }

        bestTime = PlayerPrefs.GetFloat("BestTime");
        UITxtBestTime.text = "Best Time:   " + Math.Round(bestTime,3);

        return;
    }

    void OnTriggerEnter(Collider other) { //when the bike passed through a checkpoint it calls this function
        prevCheckPoint = curCheckPt;
        curCheckPt = other.gameObject;
        LoopCheck();

        if (curLap > totalLaps) {
            Debug.Log("User has finished the race!");

            SceneManager.LoadScene("MainMenu"); 
            Debug.Log("Switching to Main Menu scene");
        }

        UITxtLap.text = "Lap " + curLap + " of " + totalLaps;
    }

    void LoopCheck() {
        if (curCheckPt != null) {
            if (prevCheckPoint == curCheckPt) {
                Debug.LogWarning("User tried to pass through the same checkpoint twice.");
                return;
            }
            if (curCheckPt == SFLine) { // starts timing loop at Start-Finish Line
                numCPPassed = 1;
                curTime = 0;
                startTime = Time.time;
            }
            if (numCPPassed >= (CheckPoints.Length - 1) && curCheckPt == SFLine) { 
                endTime = Time.time - startTime;
                // calculates end timing loop at Start-Finish Line after final Checkpoint crossed
                ChangeBestTime(endTime);
                numCPPassed = 1;
                curLap++;
            }
            numCPPassed++;
            return;
        }
    }

    void OnDrawGizmos() {
        if (resetBestLapTime) {
            resetBestLapTime = false;
            PlayerPrefs.SetFloat("BestTime", 99999.999f);
            Debug.LogWarning("Best Lap Time has been reset.");
        }
    }
}
