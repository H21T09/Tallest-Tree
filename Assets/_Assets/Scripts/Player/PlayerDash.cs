using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10f; 
    private Rigidbody2D rb;
    public  bool IsDash;
    public  bool Dasing;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dash"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = new Vector2(0, dashForce);
            IsDash = true;
            Dasing = true;

            float rotationAngle = transform.localScale.x == 1 ? 90f : -90f;
            rb.MoveRotation(rotationAngle);
        }
    }



}
