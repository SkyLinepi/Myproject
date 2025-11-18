using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
     [Tooltip("Movement speed in units per second")]
            public float speed = 5f;
            public Rigidbody2D rb2d;
            
            public int mp = 1;
             
            public Collider2D targetCollider;

            [Tooltip("When true and a Rigidbody/Rigidbody2D is present, movement uses its MovePosition (better for physics).")]
            public bool preferRigidbody = true;

            [Tooltip("Distance to consider 'arrived' at the target")]

            
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0f )); // rb2d คือ Rigidbody2D เเละเข้าถึง Component ของ Rigidbody2D 
        // ซึ่งมี ฟังก์ชัน AddForce
        Vector2 mp = Input.mousePosition;

        rb2d.AddForce(mp,0);
    }
}
