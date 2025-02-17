using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharracterJump : MonoBehaviour
{
    public float jumpForce;
    public float forwardForce;
    public bool isGrounded;
    public Rigidbody2D rb;
    private float defaultRotationZ = 0f;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            TurnAround();
            Jump();
        }
        if (!isGrounded)
        {
            transform.rotation = Quaternion.Euler(0, 0, defaultRotationZ);
        }

    }

    void Jump()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isGrounded = false;
        rb.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        float direction = transform.localScale.x;
        Vector2 jumpDirection = new Vector2(forwardForce * direction, jumpForce);
        rb.AddForce(jumpDirection, ForceMode2D.Impulse);
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

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            float platformRotationZ = collision.transform.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0, 0, platformRotationZ);
            isGrounded = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.rotation = Quaternion.Euler(0, 0, defaultRotationZ);
        }
    }

    
}
