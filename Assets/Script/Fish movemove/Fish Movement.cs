using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float minSpeed = 1f;    
    public float maxSpeed = 3f;    
    private float speed;           

    [Header("Movement Area")]
    public float minX = -7f;
    public float maxX = 7f;
    public float minY = -4f;
    public float maxY = 0f;

    [Header("Direction Change")]
    public float changeDirectionTime = 2f;  
    private float timer;

    private Vector2 targetDirection;  

    void Start()
    {
        
        speed = Random.Range(minSpeed, maxSpeed);

       
        PickNewDirection();
    }

    void Update()
    {
       
        transform.Translate(targetDirection * speed * Time.deltaTime);

     
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
        }

      
        StayInsideArea();

       
        FlipSprite();
    }

   
    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);  
        float rad = angle * Mathf.Deg2Rad;
        targetDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        timer = 0f;
    }

    
    void StayInsideArea()
    {
        Vector3 pos = transform.position;

        if (pos.x < minX || pos.x > maxX)
        {
            targetDirection.x = -targetDirection.x;          
            pos.x = Mathf.Clamp(pos.x, minX, maxX);         
            timer = 0f;
        }

        if (pos.y < minY || pos.y > maxY)
        {
            targetDirection.y = -targetDirection.y;          
            pos.y = Mathf.Clamp(pos.y, minY, maxY);          
            timer = 0f;
        }

        transform.position = pos;
    }

    void FlipSprite()
    {
        if (targetDirection.x == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = (targetDirection.x > 0) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
