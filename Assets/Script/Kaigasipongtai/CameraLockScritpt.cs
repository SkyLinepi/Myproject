using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    // วัตถุเป้าหมายที่กล้องจะติดตาม (ลาก Player มาใส่ใน Inspector)
    public Transform target;

    [Header("การตั้งค่าการติดตาม")]
    // ความเร็วในการตาม (ค่ามาก = ตามเร็วขึ้น)
    [Range(0.01f, 1.0f)]
    public float smoothSpeed = 0.125f; 
    
    // ระยะห่างคงที่จากเป้าหมาย (แกน Z ควรเป็นค่าลบ เช่น -10 เพื่อให้กล้องมองเห็น)
    public Vector3 offset = new Vector3(0f, 0f, -10f); 

    // ใช้ LateUpdate เพื่อให้แน่ใจว่าตามหลังการเคลื่อนที่ของ Player
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera Follow 2D: Target is not assigned.");
            return;
        }

        // 1. กำหนดตำแหน่งที่ต้องการให้กล้องไปถึง
        // ใช้ตำแหน่ง X และ Y ของเป้าหมาย และใช้ค่า Z คงที่จาก offset
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z // ตำแหน่ง Z คงที่สำหรับกล้อง 2D
        );

        // 2. ทำให้การเคลื่อนที่นุ่มนวลด้วย Lerp
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position, 
            desiredPosition, 
            smoothSpeed
        );
        
        // 3. กำหนดตำแหน่งใหม่ให้กับกล้อง
        transform.position = smoothedPosition;
    }
}