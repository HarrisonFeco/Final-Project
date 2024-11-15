using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopRightMainUI : MonoBehaviour {
    [Header("Inscribed")]

    public TextMeshProUGUI     UITxtScore;

    [Header("Dynamic")]
    private int     score = 0;

    void Start() {
        UITxtScore.text = "Score: " + score;
    }
}