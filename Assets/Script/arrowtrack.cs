using UnityEngine;

public class arrowtrack : MonoBehaviour
{
    public Transform player;           // the player or center object
    public float orbitRadius = 2f;     // how far the arrow stays from center
    public Transform arrow;            // the arrow sprite object

    void Update()
    {
        // --- 1. Get mouse world position ---
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // --- 2. Direction from player → mouse ---
        Vector3 dir = mouseWorld - player.position;
        dir.Normalize(); // important!

        // --- 3. Position arrow on circle around player ---
        arrow.position = player.position + dir * orbitRadius;

        // --- 4. Rotate arrow so it points toward mouse ---
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Adjust based on sprite default orientation:
        arrow.rotation = Quaternion.Euler(0, 0, angle- 90f);
    }
}
