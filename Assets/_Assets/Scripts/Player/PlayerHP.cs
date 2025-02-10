using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public ParticleSystem FurExplosion;
    public GameObject BodyPlayer;
    public CinemachineVirtualCamera Camera;

    public TMP_Text TextDied;
    public int DiedCount;

    public int maxLives = 3; 
    private int currentLives;

    public Image[] hearts; 
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        currentLives = maxLives;
        UpdateLifeUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sawblade"))
        {
            BodyPlayer.SetActive(false);
            Camera.enabled = false;
            FurExplosion.Play();
            DiedCount++;

            currentLives--;
            if (currentLives < 0) currentLives = 0;

            UpdateLifeUI();
        }
    }

    

    void UpdateLifeUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

        TextDied.text = DiedCount.ToString();
    }
}
