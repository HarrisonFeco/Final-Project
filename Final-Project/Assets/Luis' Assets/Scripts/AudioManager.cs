using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // Singleton pattern enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Ensure AudioSource is available
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; // Music should loop
    }

    public void PlayRandomMusic()
    {
        string clipPath;
        if (Random.Range(0, 4) == 0)
        {
            clipPath = "Crazy-phrog-184294"; // Path to Crazy Frog music (Assets/Resources/Audio/CrazyFrog.mp3)
        }
        else
        {
            clipPath = "Video-game-music-loop-27629"; // Path to default music (Assets/Resources/Audio/DefaultMusic.mp3)
        }

        AudioClip clip = Resources.Load<AudioClip>(clipPath);
        if (clip != null)
        {
            PlayClip(clip);
        }
        else
        {
            Debug.LogError($"AudioManager: Could not find audio clip at path '{clipPath}'");
        }
    }

    /// <param name="clip">The AudioClip to play.</param>
    public void PlayClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    /// <param name="volume">Volume level (0.0 to 1.0).</param>
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}