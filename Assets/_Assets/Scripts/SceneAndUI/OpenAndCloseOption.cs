using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseOption : MonoBehaviour
{
    public GameObject PanelOption;


    public void OpenHowToPlay()
    {
        PanelOption.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseHowToPlay()
    {
        PanelOption.SetActive(false);
        Time.timeScale = 1f;
    }
}
