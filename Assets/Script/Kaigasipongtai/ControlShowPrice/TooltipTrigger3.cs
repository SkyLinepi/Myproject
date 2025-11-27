using UnityEngine;
using UnityEngine.EventSystems; // ต้องใช้ namespace นี้

public class TooltipTrigger3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ตัวแปรสำหรับเก็บราคาของไอเท็มนี้
    //string price = "100 ";
    

    // ตัวแปรสำหรับอ้างอิงถึง Tooltip Manager (ต้องลากมาใส่ใน Inspector)
    public TooltipManager tooltipManager;

    // ฟังก์ชันนี้จะถูกเรียกทันทีเมื่อเมาส์เข้าสู่ขอบเขตของวัตถุ
    public void OnPointerEnter(PointerEventData eventData)
    {
        // สั่งให้ Tooltip Manager แสดง Tooltip พร้อมส่งข้อมูลราคา
        tooltipManager.ShowTooltip3(Shop.calculatePriceSpeed.ToString());
    }

    // ฟังก์ชันนี้จะถูกเรียกทันทีเมื่อเมาส์ออกจากขอบเขตของวัตถุ
    public void OnPointerExit(PointerEventData eventData)
    {
        // สั่งให้ Tooltip Manager ซ่อน Tooltip
        tooltipManager.HideTooltip3();
    }
}
