using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTrandition : MonoBehaviour
{
    public GameObject Effect;
    public Animator trandition;
    private void Awake()
    {
        Effect = GameObject.Find("Scene Trandition");
        trandition.SetTrigger("Start");
        Invoke("DelaySceneTradition", 1f);
    }


    void DelaySceneTradition()
    {
        
        Effect.SetActive(false);
    }



}
