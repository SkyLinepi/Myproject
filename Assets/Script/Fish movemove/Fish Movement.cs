using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("FishData")]
    public fish FishIdentity;

    [Header("Speed Settings")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    private float speed;

    private float minX, maxX, minY, maxY;

    [Header("Direction Change")]
    public float changeDirectionTime = 2f;
    private float timer;

    private Vector2 targetDirection;
    private SpriteRenderer spriteRenderer;


    private bool facingRightInitially;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FishIdentity.fishPic;


        facingRightInitially = transform.localScale.x > 0;

        if (FishIdentity != null)
        {
            //Debug.Log("doit");
            speed = Random.Range(minSpeed * FishIdentity.ChoasProb,
                                 maxSpeed * FishIdentity.ChoasProb);
        }
        else
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }

        PickNewDirection();
    }

    public void SetArea(SpriteRenderer area)
    {
        Bounds b = area.bounds;

        minX = b.min.x;
        maxX = b.max.x;
        minY = b.min.y;
        maxY = b.max.y;
    }

    public void SetFishData(fish data)
    {
        FishIdentity = data;
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
        if (targetDirection.x > 0)   
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (targetDirection.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D Hook)
    {
        if(Hook.gameObject.CompareTag("Hook"))
        {
            Hooked();
        }
    }

    public void Hooked()
    {
        FindObjectOfType<GameManager>().TriggerMiniGame(FishIdentity);
    }
}



