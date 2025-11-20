using UnityEngine;

public class FishingLine : MonoBehaviour
{
    public Transform rodTip;        // Where the line begins
    public Transform hook;          // The hook or bobber
    public float maxLineLength = 10f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        DrawLine();
        ClampDistance();
    }

    void DrawLine()
    {
        lineRenderer.SetPosition(0, rodTip.position);
        lineRenderer.SetPosition(1, hook.position);
    }

    void ClampDistance()
    {
        float distance = Vector3.Distance(rodTip.position, hook.position);

        if (distance > maxLineLength)
        {
            // Push the hook back so it never exceeds max length
            Vector3 dir = (hook.position - rodTip.position).normalized;
            hook.position = rodTip.position + dir * maxLineLength;
        }
    }
}
