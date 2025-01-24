using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public ParticleSystem FurExplosion;
    public GameObject BodyPlayer;
    public GameObject Camera;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sawblade"))
        {
            Debug.Log("Dead");
            BodyPlayer.SetActive(false);
            Camera.SetActive(false);
            FurExplosion.Play();
        }
    }
}
