using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class TooltipManager : MonoBehaviour
{
    // Panel/Box ที่ใช้เป็นพื้นหลัง Tooltip
    public GameObject tooltipPanel;
    public GameObject tooltipPanel2;
    public GameObject tooltipPanel3;

    // Text ที่ใช้แสดงราคา
    public Text priceText1; 
    public Text priceText2;
    public Text priceText3;

    
    public void ShowTooltip(string price )
    {
        if (tooltipPanel && tooltipPanel2 && tooltipPanel3 != null)
        {
            priceText1.text = price;
            tooltipPanel.SetActive(true);
            
            priceText2.text = price;
            tooltipPanel2.SetActive(true);

            priceText3.text = price;
            tooltipPanel3.SetActive(true);
            
        }
    }

    
    public void HideTooltip()
    {
        if (tooltipPanel && tooltipPanel2 && tooltipPanel3 != null)
        {
            tooltipPanel.SetActive(false);
            tooltipPanel2.SetActive(false);
            tooltipPanel3.SetActive(false);
        }
    }
}
