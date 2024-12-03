using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimingLoop : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject[] CheckPoints;
    public GameObject SFLine;
    public TextMeshProUGUI UITxtLap;
    public TextMeshProUGUI UITxtTime;
    public TextMeshProUGUI UITxtBestTime;
    public int totalLaps = 1;

    [Header("Dynamic")]
    [SerializeField] private GameObject curCheckPt;
    [SerializeField] private GameObject prevCheckPoint;
    [SerializeField] private int curLap = 0; 
    [SerializeField] private float startTime;
    [SerializeField] private float endTime;
    [SerializeField] private float curTime;
    [SerializeField] private int numCPPassed = 0;
    [SerializeField] private float bestTime;  

    [Tooltip("Check box to reset best lap time.")]
    public bool resetBestLapTime = false;

    [Header("Reset")]
    public GameObject playerBike; 
    private Vector3 lastCheckpointPosition; // Store the position of the last checkpoint
    private Vector3 lastCheckpointRotation;

    void Start()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime"); 
        }

        UITxtBestTime.text = "Best Time:   " + Math.Round(bestTime, 3);
        UITxtLap.text = "Lap " + curLap + " of " + totalLaps;
        lastCheckpointPosition = SFLine.transform.position;
        lastCheckpointRotation = SFLine.transform.rotation;////////
    }

    void Update()
    {
        curTime += Time.deltaTime;
        UITxtTime.text = "Current Time:   " + Math.Round(curTime, 3);
        if (Input.GetKeyDown(KeyCode.R))      // Check if "R" key is pressed to reset to last checkpoint
        {
            ResetToLastCheckpoint();
        }
    }

    void ChangeBestTime(float timeCheck)
    { // compared new time vs best lap time
        if (timeCheck < PlayerPrefs.GetFloat("BestTime"))
        { // if new time is better, save to PlayerPrefs
            PlayerPrefs.SetFloat("BestTime", timeCheck);
        }

        bestTime = PlayerPrefs.GetFloat("BestTime");
        UITxtBestTime.text = "Best Time:   " + Math.Round(bestTime, 3);
    }

    void OnTriggerEnter(Collider other)
    { //when the bike passed through a checkpoint it calls this function
        prevCheckPoint = curCheckPt;
        curCheckPt = other.gameObject;
        LoopCheck();

        // Update the last checkpoint position when crossing a new checkpoint
        if (curCheckPt != SFLine)
        {
            lastCheckpointPosition = curCheckPt.transform.position;
            lastCheckpointRotation = curCheckPt.transform.rotation;
        }

        if (curLap > totalLaps)
        {
            Debug.Log("User has finished the race!");
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Switching to Main Menu scene");
        }

        UITxtLap.text = "Lap " + curLap + " of " + totalLaps;
    }

    void LoopCheck()
    {
        if (curCheckPt != null)
        {
            if (prevCheckPoint == curCheckPt)
            {
                Debug.LogWarning("User tried to pass through the same checkpoint twice.");
                return;
            }
            if (numCPPassed >= (CheckPoints.Length - 1) && curCheckPt == SFLine)
            {
                endTime = Time.time - startTime;
                // calculates end timing loop at Start-Finish Line after final Checkpoint crossed
                ChangeBestTime(endTime);
                numCPPassed = 1;
                curLap++;
            }
            else if (curCheckPt == SFLine)
            { // starts timing loop at Start-Finish Line
                numCPPassed = 1;
                curTime = 0;
                startTime = Time.time;
            }
            numCPPassed++;
        }
    }

    void ResetToLastCheckpoint()
    {
        if (playerBike != null)
        {
            // Reset the player's position to the last checkpoint
            playerBike.transform.position = lastCheckpointPosition;  
            playerBike.transform.rotation = lastCheckpointRotation; 
            // Optionally reset the player's velocity or add any other reset logic here
            Rigidbody rb = playerBike.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Debug.Log("Player reset to last checkpoint.");
        }
    }

    void OnDrawGizmos()
    {
        if (resetBestLapTime)
        {
            resetBestLapTime = false;
            PlayerPrefs.SetFloat("BestTime", 99999.999f);
            Debug.LogWarning("Best Lap Time has been reset.");
        }
    }
}