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
        SceneManager.LoadScene("Track_1");
        Debug.Log("Switching to Track_1");
    }
    public void Track2Switch()
    {
        SceneManager.LoadScene("Track_2");
        Debug.Log("Switching to Track_2");
    }
    public void Track3Switch()
    {
        SceneManager.LoadScene("Track_3");
        Debug.Log("Switching to Track_3");
    }
}
