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
    public AudioClip audioClip;
    public AudioSource audioSource;

    public GameObject Button;

    

    
    private float P_holdStartTime = 0f; 
    private const float HoldDuration = 3f; 

    void Start()
    {
        
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
        
        
        if (Input.GetKey(KeyCode.P))
        {
            float heldTime = Time.time - P_holdStartTime; 

            
            if (ProgressHold_P_BarFill != null)
            {
                float fillAmount = Mathf.Clamp01(heldTime / HoldDuration); 
                ProgressHold_P_BarFill.fillAmount = fillAmount;
                audioSource.PlayOneShot(audioClip);
            }

            
            if (heldTime >= HoldDuration)
            {
                
                ResetToStart();
                
                
                P_holdStartTime = Time.time + 9999f;
            }
        }
             
        if (Input.GetKeyUp(KeyCode.P) && P_holdStartTime > 0f && P_holdStartTime < Time.time + 9999f) 
        {
            
            if (HoldResetUIContainer != null)
            {
                HoldResetUIContainer.SetActive(false);
                
            }
            
            P_holdStartTime = 0f;

            if(Button != null)
            {
                Button.SetActive(false);
            }
            else
            {
                
            }
        }
    }

    public void ResetToStart()
    {
        //  ตั้งค่าสถานะเกมและย้ายกล้อง
        STaticBS.GameStarted = false; // รีเซ็ตสถานะเกม
        transform.position = StartPosition.position; // ย้ายกล้อง
        
        
        if (Onof && OnofCoin && OnofSellButton != null) 
        {
             Onof.SetActive(true); 
             OnofCoin.SetActive(true);
             OnofSellButton.SetActive(true);
        }

        
        if (HoldResetUIContainer != null)
        {
            HoldResetUIContainer.SetActive(false);
        }

        
    }
    
    
}