using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointOpen : MonoBehaviour
{
    public SpriteRenderer Nest;
    public Transform Player;
    private void Awake()
    {
        Nest = GetComponentInChildren<SpriteRenderer>();
        GameObject player = GameObject.Find("PlayerCheckPoint");
        Player = player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (Player.position.y > transform.position.y) Nest.color = Color.white;
    }

}
