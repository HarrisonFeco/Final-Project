using System;
using TMPro;
using UnityEngine;
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

    [Header("Audio")]
    public AudioClip newBestTimeClip; 
    public AudioClip checkPointClip;
    private AudioSource audioSource; 

    [Header("Dynamic")]
    [SerializeField] private GameObject curCheckPt;
    [SerializeField] private GameObject prevCheckPoint;
    [SerializeField] private int curLap = 0;
    [SerializeField] private float startTime;
    [SerializeField] private float curTime;
    [SerializeField] private int numCPPassed = 0;
    [SerializeField] private float bestTime;

    [Tooltip("Check box to reset best lap time.")]
    public bool resetBestLapTime = false;

    [Header("Reset")]
    public GameObject playerBike;
    private Vector3 lastCheckpointPosition;
    private Quaternion lastCheckpointRotation;

    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", 99999.999f);
        UITxtBestTime.text = $"Best Time:   {Math.Round(bestTime, 3)}";
        UITxtLap.text = $"Lap {curLap} of {totalLaps}";

        lastCheckpointPosition = SFLine.transform.position;
        lastCheckpointRotation = SFLine.transform.rotation;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        UITxtTime.text = $"Current Time:   {Math.Round(curTime, 3)}";
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToLastCheckpoint();
        }
    }

    void ChangeBestTime(float timeCheck)
    {
        if (timeCheck < bestTime)
        {
            bestTime = timeCheck;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            UITxtBestTime.text = $"Best Time:   {Math.Round(bestTime, 3)}";

            if (newBestTimeClip != null)
            {
                audioSource.PlayOneShot(newBestTimeClip);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        prevCheckPoint = curCheckPt;
        curCheckPt = other.gameObject;
        LoopCheck();

        if (curCheckPt != SFLine)
        {
            lastCheckpointPosition = curCheckPt.transform.position;
            lastCheckpointRotation = curCheckPt.transform.rotation;
        }

        if (checkPointClip != null)
        {
            audioSource.PlayOneShot(checkPointClip);
        }

        if (curLap > totalLaps)
        {
            Debug.Log("User has finished the race!");
            SceneManager.LoadScene("MainMenu");
        }

        UITxtLap.text = $"Lap {curLap} of {totalLaps}";
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
                float endTime = Time.time - startTime;
                ChangeBestTime(endTime);
                numCPPassed = 0; 
                curLap++;
            }
            else if (curCheckPt == SFLine)
            {
                numCPPassed = 0; 
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
            playerBike.transform.position = lastCheckpointPosition;
            Vector3 checkpointForward = lastCheckpointRotation * Vector3.forward;
            Quaternion correctedRotation = Quaternion.LookRotation(checkpointForward, Vector3.up);
            playerBike.transform.rotation = correctedRotation;
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