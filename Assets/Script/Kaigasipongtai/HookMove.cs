
using UnityEngine;

public class HookMove : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public float force = 1f;

    public float DistanceToMouse;
    private Vector3 MousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetMousePos();
        swingThatShit();
        //Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorld.z = 0;
        //Vector2 direction = (mouseWorld - transform.position).normalized;
        //rb2D.AddForce(direction * force);
        //DistanceToMouse = Vector2.Distance(transform.position, mouseWorld);
    }

    public void swingThatShit()
    {
        Vector2 direction = (MousePos - transform.position).normalized;
        rb2D.AddForce(direction * force);
        DistanceToMouse = Vector2.Distance(transform.position, MousePos);
    }

    public void GetMousePos()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;
        MousePos = mouseWorld;
    }
}
