using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimingLoop : MonoBehaviour {
    [Header("Inscribed")]
    //[SerializeField] GameObject[] CheckPoints;
    public TextMeshProUGUI     UITxtLap;
    public TextMeshProUGUI     UITxtTime;
    public TextMeshProUGUI     UITxtBestTime;

    [Header("Dynamic")]
    //[SerializeField] private GameObject lastCheckpoint;
    //[SerializeField] private GameObject curCheckPt;
    [SerializeField] private int     curLap = 0;
    [SerializeField] private float   startTime;
    [SerializeField] private float   endTime;
    [SerializeField] private float   curTime;
    [SerializeField] private int     totalLaps = 1;
    [SerializeField] private float   bestTime;

    void Start() {
        if (PlayerPrefs.HasKey("BestTime")) {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }

        UITxtBestTime.text = "Best Time:   " + Math.Round(bestTime,3);
        UITxtLap.text = "Lap " + curLap + " of " + totalLaps;
    }

    void Update() {
        curTime += Time.deltaTime;

        UITxtTime.text = "Current Time:   " + Math.Round(curTime, 3);
    }

    void ChangeBestTime(float timeCheck) {
        if (timeCheck < PlayerPrefs.GetFloat("BestTime")) {
            PlayerPrefs.SetFloat("BestTime", timeCheck);
        }
        return;
    }

    /*public void LoopCheck(GameObject curtCheckPt) {
        curCheckPt = curtCheckPt;

        if (curCheckPt != null) {
            if (lastCheckpoint == curCheckPt) {
                Debug.LogWarning("User tried to pass through the same checkpoint twice.");
                return;
            }
        }
    }*/
}
