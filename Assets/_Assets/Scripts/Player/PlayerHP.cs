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
    public GameObject PanelLose;

    public Image[] hearts; 
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public AudioClip soundEffect;
    public AudioSource audioSource;

    private GameObject FindInChildren(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child.gameObject;

            GameObject found = FindInChildren(child, name);
            if (found != null)
                return found;
        }
        return null;
    }


    private void Awake()
    {
        GameObject parentObject = GameObject.Find("UIGamePlay");
        PanelLose = FindInChildren(parentObject.transform, "Panel Out Of Attemp");

        
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;

    }
    void Start()
    {
        currentLives = maxLives;
        UpdateLifeUI();
    }

    private void Update()
    {
        DelayOpenUILose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sawblade"))
        {
            BodyPlayer.SetActive(false);
            Camera.enabled = false;
            FurExplosion.Play();
            DiedCount++;
            audioSource.Play();
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

    void DelayOpenUILose()
    {
        if (currentLives == 0)
        {
            Invoke("OpenUILose", 0.5f);
        }
    }

    void OpenUILose()
    {
        PanelLose.SetActive(true);

    }
}

