using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject PanelHowToPlay;
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

    


}
