using UnityEngine;

public class WaterTriggerHandler  : MonoBehaviour
{
    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private GameObject _splashParticles;

    private EdgeCollider2D _edgeColl;
    private InteractableWater _water;

    private void Awake()
    {
        _edgeColl = GetComponent<EdgeCollider2D>();
        _water = GetComponentInParent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((_waterMask.value & (1 << collision.gameObject.layer)) == 0)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // สร้างเอฟเฟกต์น้ำกระเซ็น
                Vector2 loaclePos = gameObject.transform.localPosition;
                Vector2 hitObjectPos = collision.transform.position;
                Bounds hitObjectBounds = collision.bounds;

                Vector3 spawnPos = Vector3.zero;
                if (collision.transform.position.y >= _edgeColl.points[1].y + _edgeColl.offset.y + loaclePos.y)
                {
                    // วัตถุชนจากด้านบน
                    spawnPos = hitObjectPos - new Vector2(0f, hitObjectBounds.extents.y);            
                }
                else
                {
                    // วัตถุชนจากด้านล่าง
                    spawnPos = hitObjectPos + new Vector2(0f, hitObjectBounds.extents.y);
                }           

                Instantiate(_splashParticles, spawnPos, Quaternion.identity);
            }
            // ยึดจุดกระเซ็นให้ถึงความเร็วสูงสุด

            int multiplier = 1;
            if (rb.linearVelocity.y < 0)
            {
                multiplier = -1;
            }
            else
            {
                multiplier = 1;
            }
            
            float vel = rb.linearVelocity.y * _water.ForceMultiplier;
            vel *= multiplier;
        }
    }
}
