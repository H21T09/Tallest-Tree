using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    public bool IsWin = false;
    public Rigidbody2D Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsWin = true;
            Player.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log("WIN");
        }
    }
}
