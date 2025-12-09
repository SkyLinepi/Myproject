using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameManager gameManager;
    public MoneyDisplay moneyDisplay;
    public BackpackUIController backpackUIController;


    [Header("Shop Settings")]
    [SerializeField] private GameObject SubMenuContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TriggerSubMenu()
    {
        bool isActive = SubMenuContainer.activeSelf;
        SubMenuContainer.SetActive(!isActive);
    }

    public void SellAllfish()
    {
        if (gameManager == null || gameManager.fishBackpack == null)
        {
            Debug.LogError("GameManager or Fish Backpack is not assigned/initialized.");
            return;
        }

        int totalProfit = 0;

        // 1. วนลูปผ่าน Fish Backpack 
        for (int i = 0; i < gameManager.fishBackpack.Length; i++)
        {
            // ตรวจสอบว่าช่องกระเป๋ามีข้อมูลปลาอยู่หรือไม่
            if (gameManager.fishBackpack[i] != null)
            {
                // ดึงราคาขาย (Sellprice) ของปลาแต่ละตัว (e.g. Angler Lv.1 = 210)
                int fishSellPrice = gameManager.fishBackpack[i].Sellprice;
                totalProfit += fishSellPrice;

                // ล้างข้อมูลปลาออกจากช่องกระเป๋า (ทำให้ปลาหายไปจากข้อมูลเกม)
                gameManager.fishBackpack[i] = null;
            }
        }

        // เพิ่มเงินที่ได้ทั้งหมดเข้าในตัวแปร Money ของ GameManager (public int Money)
        gameManager.Money += totalProfit;

        Debug.Log($"Sold all fish for {totalProfit} gold. New total money: {gameManager.Money}");

        // อัปเดต UI (แสดงการเปลี่ยนแปลงแก่ผู้เล่น)

        // อัปเดต UI เงิน
        if (moneyDisplay != null)
        {
            moneyDisplay.UpdateMoneyUI();
        }

        // อัปเดต UI กระเป๋า (เพื่อให้รูปปลาในช่องกระเป๋าหายไป)
        if (backpackUIController != null)
        {
            backpackUIController.UpdateBackpackUI();
        }
    }

}

