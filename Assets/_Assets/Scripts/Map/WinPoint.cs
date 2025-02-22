using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPoint : MonoBehaviour
{
    public bool IsWin = false;
    public Rigidbody2D Player;
    public GameObject PanelWin;
    public ParticleSystem Effect;
    public AudioClip soundEffect;
    private AudioSource audioSource;

    private void Awake()
    {
        Effect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsWin = true;
            Player.velocity = Vector2.zero;
            Effect.Play();
            Player.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("WIN");
            PanelWin.SetActive(true);
            audioSource.Play();
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            Debug.Log(currentLevel);

            PlayerPrefs.SetInt("LevelCompleted_" + currentLevel, 1);
            if (currentLevel >= unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            }
            FindObjectOfType<GameCompletionManager>().CompleteLevel();
            PlayerPrefs.SetInt("LastSelectedButton", currentLevel + 1);
            PlayerPrefs.SetInt("LastTeleportIndex", currentLevel);
            PlayerPrefs.Save();


        }
    }
}
