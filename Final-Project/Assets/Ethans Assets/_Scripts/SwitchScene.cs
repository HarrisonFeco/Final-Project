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
    public void Track1Switch()
    {
        SceneManager.LoadScene("Test_Track");
        Debug.Log("Switching to Test_Track scene");
    }
    public void Track2Switch()
    {
        SceneManager.LoadScene("Test_Track2");
        Debug.Log("Switching to Test_Track2 scene");
    }
}
