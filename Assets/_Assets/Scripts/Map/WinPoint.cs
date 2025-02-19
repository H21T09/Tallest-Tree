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

    private void Awake()
    {
        Effect = GetComponentInChildren<ParticleSystem>();
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

            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            PlayerPrefs.SetInt("LevelCompleted_" + currentLevel, 1);
            if (currentLevel >= unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            }

            PlayerPrefs.Save();


        }
    }
}
