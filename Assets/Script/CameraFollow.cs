using UnityEngine;
using UnityEngine.UI; 

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerPosition;
    public Transform StartPosition;
    public Vector3 offset;
    
    [Header ("ตัวเเปรเปิดปิดหลังจาก กด Reset เเล้ว")]  
    public GameObject Onof; // ตัวเเปรเปิดปิดปุ่มหลังจาก Reset 
    public GameObject OnofSellButton;
    public GameObject OnofCoin;

    public Image ProgressHold_P_BarFill;

    public GameObject HoldResetUIContainer;

    

    
    private float P_holdStartTime = 0f; // ตัวแปรเก็บเวลาที่เริ่มกดปุ่ม P
    private const float HoldDuration = 3f; // ตัวเเปรเวลาที่ต้องกดปุ่ม P ค้างไว้ (3 วินาที)

    void Start()
    {
        // ควรตั้งค่าตำแหน่งเริ่มต้นและซ่อน UI ตั้งแต่เริ่มเกม
        transform.position = StartPosition.position;
        if (HoldResetUIContainer != null)
        {
             HoldResetUIContainer.SetActive(false);
        }
    }

    void Update()
    {
       
        if(STaticBS.GameStarted == false) 
        {
            TrackPosition();
        }
        else
        {
            TrackPlayer();
        }

        HandleResetHold();
    }

    public void TrackPlayer()
    {
        Vector3 CamPos = PlayerPosition.position + offset;
        
        Vector3 SmoothCam = Vector3.Lerp(transform.position, CamPos, 0.05f); 
        transform.position = SmoothCam;
    }

    public void TrackPosition()
    {
        transform.position = StartPosition.position;
    }

    public void HandleResetHold()
    {
        // 1. ตรวจสอบเมื่อเริ่มกดปุ่ม 'P' ลงไป
        if (Input.GetKeyDown(KeyCode.P))
        {
            // บันทึกเวลาเริ่มต้นและแสดง UI
            P_holdStartTime = Time.time; 
            if (HoldResetUIContainer != null) 
            {
                HoldResetUIContainer.SetActive(true);
            }
            if (ProgressHold_P_BarFill != null)
            {
                ProgressHold_P_BarFill.fillAmount = 0f;
            }
        }
        
        // ตรวจสอบทุกเฟรมเมื่อกดปุ่ม 'P' ค้างอยู่
        if (Input.GetKey(KeyCode.P))
        {
            float heldTime = Time.time - P_holdStartTime; // คำนวณเวลาที่กดค้าง

            // อัปเดตหลอดความคืบหน้า
            if (ProgressHold_P_BarFill != null)
            {
                float fillAmount = Mathf.Clamp01(heldTime / HoldDuration); // คำนวณ 0.0 ถึง 1.0
                ProgressHold_P_BarFill.fillAmount = fillAmount;
            }

            // ตรวจสอบการรีเซ็ตเมื่อครบเวลา
            if (heldTime >= HoldDuration)
            {
                // รีเซ็ตเกม
                ResetToStart();
                
                // ป้องกันการรีเซ็ตซ้ำโดยกำหนดเวลาเริ่มต้นให้เป็นค่ามาก
                P_holdStartTime = Time.time + 9999f;
            }
        }
        
        //  ตรวจสอบเมื่อปล่อยปุ่ม P หรือเมื่อเกมรีเซ็ตไปแล้ว
        // โค้ดเดิมของคุณซับซ้อนไป ผมจึงแก้ไขให้ง่ายขึ้น
        if (Input.GetKeyUp(KeyCode.P) && P_holdStartTime > 0f && P_holdStartTime < Time.time + 9999f) 
        {
            // ซ่อนหลอดและข้อความ(ถ้าปล่อยปุ่มก่อนครบ 3 วินาที)
            if (HoldResetUIContainer != null)
            {
                HoldResetUIContainer.SetActive(false);
            }
            // ล้างค่าเวลาที่เริ่มกดค้างไว้
            P_holdStartTime = 0f; 
        }
    }

    public void ResetToStart()
    {
        //  ตั้งค่าสถานะเกมและย้ายกล้อง
        STaticBS.GameStarted = false; // รีเซ็ตสถานะเกม
        transform.position = StartPosition.position; // ย้ายกล้อง
        
        //  เปิด UI หลักกลับคืนมา
        if (Onof && OnofCoin && OnofSellButton != null) 
        {
             Onof.SetActive(true); 
             OnofCoin.SetActive(true);
             OnofSellButton.SetActive(true);
        }

        //  ซ่อนหลอดรีเซ็ต
        if (HoldResetUIContainer != null)
        {
            HoldResetUIContainer.SetActive(false);
        }
    }
    
    
}