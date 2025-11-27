using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class TooltipManager : MonoBehaviour
{
    // Panel/Box ที่ใช้เป็นพื้นหลัง Tooltip
    public GameObject tooltipPanel;

    // Text ที่ใช้แสดงราคา
    public Text priceText; 

    
    public void ShowTooltip(string price)
    {
        if (tooltipPanel != null)
        {
            priceText.text = price;
            tooltipPanel.SetActive(true);
            
            
        }
    }

    
    public void HideTooltip()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }
}
