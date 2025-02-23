using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public AudioClip soundEffect;
    public AudioSource audioSource;

    public GameObject PanelOption;
    public GameObject TreeOption;
    public GameObject Daily;
    public GameObject Achievements;

    private void Awake()
    {
        GameObject parentObject = GameObject.Find("UI");
        PanelOption = parentObject.transform.Find("Panel Options")?.gameObject;
        
        audioSource.playOnAwake = false;
        audioSource.clip = soundEffect;
    }

   

    public void OpenOptionInGame()
    {
        PanelOption.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenOption()
    {
        PanelOption.SetActive(true);
    }


    public void CloseOption()
    {
        PanelOption.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackAgain()
    {
        PanelOption.SetActive(false);
    }

    public void OpenDaily()
    {
        Daily.SetActive(true);
    }

    public void CloseDaily()
    {
        Daily.SetActive(false);
    }

    public void OpenTree()
    {
        TreeOption.SetActive(true) ;
        audioSource.Play();
    }

    public void CloseTree()
    {
        TreeOption.SetActive(false) ;
    }

    public void OpenAchievements()
    {
        Achievements.SetActive(true);
    }

    public void CloseAchievements()
    { 
       Achievements.SetActive(false);
    }
}
