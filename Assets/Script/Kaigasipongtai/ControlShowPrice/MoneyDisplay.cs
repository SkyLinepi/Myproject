using UnityEngine;
using UnityEngine.UI; // สำหรับ Text Legacy

public class MoneyDisplay : MonoBehaviour
{
    // ต้องลาก Text Component (Legacy) มาใส่ใน Inspector
    public Text moneyText; 

    // ต้องลาก GameObject ที่มี GameManager มาใส่ใน Inspector
    public GameManager gameManager; 
    
    // อ้างอิงถึง GameObject ที่เป็นแผงแสดงผลเงินทั้งหมด (เช่น แผงที่มีเหรียญและตัวเลข)
    // นี่คือส่วนที่คุณจะเปิด/ปิด
    public GameObject moneyDisplayPanel; 

    // ฟังก์ชันสำหรับดึงค่าเงินและอัปเดต Text
    public void UpdateMoneyUI()
    {
        if (gameManager == null || moneyText == null) return;
        
        int currentMoney = gameManager.Money; 
        
        // จัดรูปแบบตัวเลขให้มีจุลภาค (เช่น 10000 เป็น 10,000)
        moneyText.text = currentMoney.ToString("N0"); 
    }
    
    // ฟังก์ชันที่จะถูกเรียกเมื่อผู้เล่นกดปุ่มเปิด/ปิด
    public void ToggleMoneyDisplay()
    {
        if (moneyDisplayPanel == null) return;

        // ตรวจสอบสถานะปัจจุบันแล้วสลับ (เปิด > ปิด, ปิด > เปิด)
        bool isActive = moneyDisplayPanel.activeSelf;
        moneyDisplayPanel.SetActive(!isActive);

        // ถ้ากำลังจะเปิดแสดงผล ให้เรียก UpdateMoneyUI() ทันที
        // เพื่อให้แน่ใจว่าตัวเลขเป็นปัจจุบันที่สุดก่อนแสดง
        if (!isActive)
        {
            UpdateMoneyUI();
        }
    }

    void Start()
    {
        // ตรวจสอบให้แน่ใจว่าถ้าแผงเปิดอยู่ ก็แสดงค่าเงินที่ถูกต้อง
        if (moneyDisplayPanel != null && moneyDisplayPanel.activeSelf)
        {
            UpdateMoneyUI();
        }
    }
}