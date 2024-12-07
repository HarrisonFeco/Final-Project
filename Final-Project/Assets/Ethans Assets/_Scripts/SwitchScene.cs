using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void MainMenuSwitch()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Switching to Main Menu scene");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(1.0f); // Reset to full volume
            AudioManager.Instance.PlayRandomMusic();
        }
    }

    public void SettingsMenuSwitch()
    {
        SceneManager.LoadScene("Settings");
        Debug.Log("Switching to Settings scene");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(1.0f); // Reset to full volume
            AudioManager.Instance.PlayRandomMusic();
        }
    }

    public void TrackSwitch(string trackSceneName)
    {
        SceneManager.LoadScene(trackSceneName);
        Debug.Log($"Switching to {trackSceneName}");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume(0.5f); // Reduce volume for tracks
            AudioManager.Instance.PlayRandomMusic();
        }
    }

    public void QuitGame() {
        Debug.Log("Quiting the game. Thanks for playing!");
        Application.Quit();
    }
}