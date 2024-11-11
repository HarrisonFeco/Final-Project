using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {
    public void MainMenuSwitch()
    {
        SceneManager.LoadScene("MainMenu"); 
        Debug.Log("Switching to Main Menu scene");
    }
    public void SettingsMenuSwitch()
    {
        SceneManager.LoadScene("Settings"); 
        Debug.Log("Switching to Settings scene");
    }
}
