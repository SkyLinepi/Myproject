using UnityEngine;

public class StartUpDown : MonoBehaviour
{
    public float speed = 2f;
    public float height = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(!STaticBS.GameStarted)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * speed) * height; 
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
        else
        {
            
        }

    }
}