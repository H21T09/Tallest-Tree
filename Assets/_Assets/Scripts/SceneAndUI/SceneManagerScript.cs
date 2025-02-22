using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject SceneEffect;
    public Animator animator;

    public AudioClip soundEffect;
    private AudioSource audioSource;

    private void Awake()
    {
        SceneEffect = GameObject.Find("Scene Trandition");
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;
    }
    public void LoadMenuScene()
    {
        audioSource.Play();
        SceneEffect.SetActive(true);
        animator.SetTrigger("End");
        Invoke("WaitToLoad", 0.5f);
        ButtonTeleportManager.SetReturningFromGame();
    }

    void WaitToLoad()
    {
        SceneManager.LoadScene("MenuGame1");
    }

}
