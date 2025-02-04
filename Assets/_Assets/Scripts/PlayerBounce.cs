using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    public float bounceForce = 5f; // Lực bật ngược lại
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.velocity = Vector2.zero; // Đặt vận tốc về 0 trước khi bật lại
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
        }
    }
}
