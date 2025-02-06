using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10f; 
    private Rigidbody2D rb;
    public  bool IsDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dash"))
        {
            rb.velocity = new Vector2(0, dashForce);
            IsDash = true;
            if (transform.localScale.x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
}
