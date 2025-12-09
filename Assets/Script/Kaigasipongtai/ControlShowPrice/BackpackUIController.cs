// ใน BackpackUIController.cs

using UnityEngine;
using UnityEngine.UI; 

public class BackpackUIController : MonoBehaviour
{
    public GameManager gameManager; 
    public Image[] backpackSlots; // ลาก Image Component ของช่องกระเป๋าทั้งหมดมาใส่

    public void UpdateBackpackUI()
    {
        if (gameManager == null || backpackSlots.Length != gameManager.fishBackpack.Length) return;

        for (int i = 0; i < backpackSlots.Length; i++)
        {
            // ถ้าข้อมูลในกระเป๋าเป็น null (ถูกขายไปแล้ว)
            if (gameManager.fishBackpack[i] == null)
            {
                // ซ่อนรูปภาพใน UI โดยการทำให้โปร่งใส
                backpackSlots[i].color = Color.clear; 
            }
            else
            {
                // ถ้ามีปลาอยู่ ให้แสดงรูปปลา
                backpackSlots[i].sprite = gameManager.fishBackpack[i].fishPic;
                backpackSlots[i].color = Color.white;
            }
        }
    }
}