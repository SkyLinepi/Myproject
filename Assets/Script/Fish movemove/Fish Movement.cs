using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    public float minSpeed = 1f;    // ความเร็วต่ำสุด
    public float maxSpeed = 3f;    // ความเร็วสูงสุด
    private float speed;           // ความเร็วของปลาตัวนี้

    [Header("Movement Area")]
    public float minX = -7f;
    public float maxX = 7f;
    public float minY = -4f;
    public float maxY = 0f;

    [Header("Direction Change")]
    public float changeDirectionTime = 2f;  // เปลี่ยนทิศทุกกี่วินาที
    private float timer;

    private Vector2 targetDirection;  // ทิศทางการว่าย

    void Start()
    {
        // กำหนดความเร็วสุ่มให้ปลาแต่ละตัว
        speed = Random.Range(minSpeed, maxSpeed);

        // สุ่มทิศทางแรก
        PickNewDirection();
    }

    void Update()
    {
        // เคลื่อนที่ไปตามทิศทาง
        transform.Translate(targetDirection * speed * Time.deltaTime);

        // อัพเดตเวลา
        timer += Time.deltaTime;
        if (timer >= changeDirectionTime)
        {
            PickNewDirection();
        }

        // ตรวจสอบไม่ให้ออกนอกขอบเขตน้ำ
        StayInsideArea();

        // ปรับ Sprite ให้หันตามทิศทาง
        FlipSprite();
    }

    // เลือกทิศทางสุ่มใหม่
    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);  // สุ่มมุม 0–360 องศา
        float rad = angle * Mathf.Deg2Rad;
        targetDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        timer = 0f;
    }

    // ทำให้ปลาอยู่ในพื้นที่น้ำ
    void StayInsideArea()
    {
        Vector3 pos = transform.position;

        if (pos.x < minX || pos.x > maxX)
        {
            targetDirection.x = -targetDirection.x;           // เด้งกลับแนวนอน
            pos.x = Mathf.Clamp(pos.x, minX, maxX);          // อยู่ในขอบ
            timer = 0f;
        }

        if (pos.y < minY || pos.y > maxY)
        {
            targetDirection.y = -targetDirection.y;           // เด้งกลับแนวตั้ง
            pos.y = Mathf.Clamp(pos.y, minY, maxY);          // อยู่ในขอบ
            timer = 0f;
        }

        transform.position = pos;
    }

    // พลิก Sprite ให้หันตามทิศทาง X
    void FlipSprite()
    {
        if (targetDirection.x == 0) return;

        Vector3 scale = transform.localScale;
        scale.x = (targetDirection.x > 0) ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
