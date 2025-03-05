using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCountGame : MonoBehaviour
{
    public float TimeCountInGame;
    public TMP_Text TimeText;
    public GameObject PanelWin;

    private void Update()
    {
        TimeText.text = TimeCountInGame.ToString();
        TimeCount();
    }

    void TimeCount()
    {
        if (!PanelWin.activeSelf)
            TimeCountInGame = Time.timeSinceLevelLoad;
        else return;
    }



}
