using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject SceneEffect;
    public Animator animator;

    private void Awake()
    {
        SceneEffect = GameObject.Find("Scene Trandition");
    }
    public void LoadMenuScene()
    {
        SceneEffect.SetActive(true);
        animator.SetTrigger("End");
        Invoke("WaitToLoad", 1f);
    }

    void WaitToLoad()
    {
        SceneManager.LoadScene("MenuGame");
    }

}
