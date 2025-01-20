using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharracterJump : MonoBehaviour
{
    public float jumpForce;
    public float forwardForce;
    public bool isGrounded;
    public  Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            TurnAround();
            Jump();
        }
    }

    void Jump()
    {
        isGrounded = false;
        rb.velocity = Vector2.zero;
        float direction = transform.localScale.x;
        Vector2 jumpDirection = new Vector2(forwardForce * direction, jumpForce);
        rb.AddForce(jumpDirection,ForceMode2D.Impulse);
    }

    void TurnAround()
    {
        if (transform.localScale.x == 1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.localScale.x == -1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
