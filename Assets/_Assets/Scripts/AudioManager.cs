using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    void Start()
    {
        ApplyAudioState();
    }

    public static void UpdateAudioState()
    {
        AudioManager[] managers = FindObjectsOfType<AudioManager>();
        foreach (AudioManager manager in managers)
        {
            manager.ApplyAudioState();
        }
    }

    void ApplyAudioState()
    {
        bool isMuted = PlayerPrefs.GetInt("AudioMuted", 0) == 1;

        foreach (AudioSource audio in audioSources)
        {
            if (audio != null)
                audio.mute = isMuted;
        }
    }
}
