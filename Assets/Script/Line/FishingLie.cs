using UnityEngine;

public class FishingLie : MonoBehaviour
{
    public Transform startPoint;   // shootpoint
    public Transform endPoint;     // fihsssss (hook)

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false; 
    }

    void Update()
    {
        
        if (!GameManager.miniGameActive)
        {
            if (line.enabled) line.enabled = false;
            return;
        }

       
        if (!line.enabled) line.enabled = true;

       
        if (startPoint == null || endPoint == null) return;

    
        line.SetPosition(0, startPoint.position);
        line.SetPosition(1, endPoint.position);
    }
}
