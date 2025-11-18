
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float force = 1f;

    public float DistanceToMouse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        Vector2 direction = (mouseWorld - transform.position).normalized;

        // ไล่ตามเมาส์s
        rb2D.AddForce(direction * force);

        DistanceToMouse = Vector2.Distance(transform.position, mouseWorld);

        // ถ้าเข้าใกล้มากเกินไป → เบรก
        if (DistanceToMouse < 0.5f)
        {
            rb2D.linearVelocity = Vector2.zero;   // หยุดนิ่ง
        }
        
    }   
}
