using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameManager gameManager;
    public MoneyDisplay moneyDisplay;
    public BackpackUIController backpackUIController;


    [Header("Shop Settings")]
    public GameObject SubMenuContainer;
    public AudioClip audioClip;
    public AudioSource audioSource;

    

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
        audioSource.PlayOneShot(audioClip);


        int totalProfit = 0;

        
        
        for (int i = 0; i < gameManager.fishBackpack.Length; i++)
        {
            
            if (gameManager.fishBackpack[i] != null)
            {
                // ดึงราคาขาย (Sellprice) ของปลาแต่ละตัว (e.g. Angler Lv.1 = 210)
                int fishSellPrice = gameManager.fishBackpack[i].Sellprice;
                totalProfit += fishSellPrice;

                
                gameManager.fishBackpack[i] = null;
            }
        }

        
        gameManager.Money += totalProfit;

        Debug.Log($"Sold all fish for {totalProfit} gold. New total money: {gameManager.Money}");

        

        
        if (moneyDisplay != null)
        {
            moneyDisplay.UpdateMoneyUI();
        }

        
        if (backpackUIController != null)
        {
            backpackUIController.UpdateBackpackUI();
        }
    }

}

