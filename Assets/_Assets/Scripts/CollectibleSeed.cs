using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSeed : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public SpriteRenderer Collectible;
    public SpriteRenderer Bubble;

    public Transform Player;
    public float magneticForce; //lực hút
    public float pickupRange;
    private bool isInRange;
    private void Update()
    {
        Check();  
    }

    void Check()
    {
        float Distance = Vector2.Distance(transform.position, Player.position);
        if (Distance < pickupRange)
        {
            isInRange= true;
        }
        else isInRange = false;

        if (isInRange)
        {
            MoveTowardPlayer();
        }
    }

    void MoveTowardPlayer()
    {
        Vector2 direction = (Player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, Player.position, magneticForce * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            particleEffect.Play();
            Collectible.enabled = false;
            Bubble.enabled = false;

            Destroy(gameObject, 0.5f);
        }
        
    }
}
