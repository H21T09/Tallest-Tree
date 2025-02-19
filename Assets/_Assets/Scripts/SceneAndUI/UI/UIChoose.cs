using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChoose : MonoBehaviour
{
    public GameObject PanelChoose;

   public void BackMenu()
    {
        PanelChoose.SetActive(false);
    }
}
