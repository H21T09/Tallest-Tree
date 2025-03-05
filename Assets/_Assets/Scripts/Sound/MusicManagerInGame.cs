using UnityEngine;

public class MusicManagerInGame : MonoBehaviour
{
    public AudioSource[] musicSources;

    void Start()
    {
        ApplyMusicState();
    }

    public static void UpdateMusicState()
    {
        MusicManagerInGame[] managers = FindObjectsOfType<MusicManagerInGame>();
        foreach (MusicManagerInGame manager in managers)
        {
            manager.ApplyMusicState();
        }
    }

    void ApplyMusicState()
    {
        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        foreach (AudioSource music in musicSources)
        {
            if (music != null)
                music.mute = isMuted;
        }
    }
}
