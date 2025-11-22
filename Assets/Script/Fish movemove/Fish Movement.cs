using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("FishData")]
    public fish FishIdentity;

    [Header("Speed Settings")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private float speed;

    [Header("Area Sprite (กรอบพื้นที่ที่ปลาอยู่ได้)")]
    public SpriteRenderer areaSprite; 

    private float minX, maxX, minY, maxY;

    [Header("Direction Change")]
    public float changeDirectionTime = 2f;
    private float timer;

    private Vector2 targetDirection;

    void Start()
    {
        speed = Random.Range(minSpeed*FishIdentity.ChoasProb, maxSpeed*FishIdentity.ChoasProb);

        SetupAreaFromSprite();

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

    
    void SetupAreaFromSprite()
    {
        if (areaSprite == null)
        {
            Debug.LogError("FishMovement: areaSprite is not assigned!");
            return;
        }

        Bounds b = areaSprite.bounds;

        minX = b.min.x;
        maxX = b.max.x;
        minY = b.min.y;
        maxY = b.max.y;
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
