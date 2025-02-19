using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public GameObject PanelOption;

    private void Awake()
    {
        GameObject parentObject = GameObject.Find("UI");
        PanelOption = parentObject.transform.Find("Panel Options")?.gameObject;
    }

    public void OpenOption()
    {
        PanelOption.SetActive(true);
        Time.timeScale = 0;
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
}
