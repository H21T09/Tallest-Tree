using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIControl : MonoBehaviour
{
    
    public GameObject PanelHowToPlay;
    public GameObject PanelOption;

    public void OpenHowToPlay()
    {
        PanelHowToPlay.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseHowToPlay()
    {
        PanelHowToPlay.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenOptiony()
    {
        PanelOption.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseOptiony()
    {
        PanelOption.SetActive(false);
        Time.timeScale = 1f;
    }










}
