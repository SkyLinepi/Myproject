using UnityEngine;

public class FishingLine2D : MonoBehaviour
{
    public Transform rodTip;          // Start of the line
    public Rigidbody2D hookRb;        // Hook/Bobber Rigidbody2D
    public float maxLineLength = 5f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void FixedUpdate()
    {
        ClampPhysics2D();
        DrawLine();
    }

    void DrawLine()
    {
        lineRenderer.SetPosition(0, rodTip.position);
        lineRenderer.SetPosition(1, hookRb.position);
    }

    void ClampPhysics2D()
    {
        Vector2 rodPos = rodTip.position;
        Vector2 hookPos = hookRb.position;

        Vector2 direction = hookPos - rodPos;
        float distance = direction.magnitude;

        // Only correct if the hook is too far
        if (distance > maxLineLength)
        {
            Vector2 dir = direction.normalized;

            // 1. Move the hook back to max allowed distance
            hookRb.position = rodPos + dir * maxLineLength;

            // 2. Kill the outward velocity
            Vector2 velocity = hookRb.linearVelocity;

            float outwardSpeed = Vector2.Dot(velocity, dir);
            if (outwardSpeed > 0)
            {
                // Remove only the outward (stretching) component
                hookRb.linearVelocity = velocity - dir * outwardSpeed;
            }
        }
    }
}
