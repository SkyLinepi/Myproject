using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float force = 1f;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(moveInput, 0).normalized;
        rb2D.AddForce(direction * force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
