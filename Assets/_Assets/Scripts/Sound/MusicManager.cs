using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource musicSource;

    private string[] menuScenes = { "MenuGame1", "MenuGame2", "MenuGame3", "MenuGame4" }; // Danh sách scene menu

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckMusicStatus(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckMusicStatus(scene.name);
    }

    private void CheckMusicStatus(string sceneName)
    {
        bool isMenuScene = System.Array.Exists(menuScenes, scene => scene == sceneName);
        bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;

        if (isMenuScene)
        {
            if (!isMuted && !musicSource.isPlaying)
                musicSource.Play();
            else if (isMuted && musicSource.isPlaying)
                musicSource.Stop();
        }
        else
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
        }
    }

    public static void UpdateMusicState()
    {
        if (Instance != null)
        {
            bool isMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            string[] menuScenes = { "MenuGame1", "MenuGame2", "MenuGame3", "MenuGame4" };

            if (isMuted && menuScenes.Contains(currentScene))
                Instance.musicSource.Stop();
            else if (!Instance.musicSource.isPlaying)
                Instance.musicSource.Play();
        }
    }
}
