using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDash : MonoBehaviour
{
    public ParticleSystem Effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Effect.Play();
        }
    }
}
