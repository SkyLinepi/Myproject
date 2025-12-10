using UnityEngine;

public class PlayerDeep : MonoBehaviour
{
    public float CurrentDepth { get; private set; }
    void Update()
    {
        
        CurrentDepth = transform.position.y;
        
    }
}
