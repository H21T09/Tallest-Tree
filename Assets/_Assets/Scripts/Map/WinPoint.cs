using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    public bool IsWin = false;
    public Rigidbody2D Player;
    public GameObject PanelWin;
    public ParticleSystem Effect;

    private void Awake()
    {
        Effect = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsWin = true;
            Player.velocity = Vector2.zero;
            Effect.Play();
            Player.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("WIN");
            PanelWin.SetActive(true);
        }
    }
}
