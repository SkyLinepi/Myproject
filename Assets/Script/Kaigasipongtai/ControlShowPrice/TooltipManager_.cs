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


    public void ShowTooltip(string price)
    {
        if (tooltipPanel != null)
        {
            priceText1.text = price;
            tooltipPanel.SetActive(true);

        }
    }

    public void ShowTooltip2(string price)
    {
        if (tooltipPanel2 != null)
        {

            priceText2.text = price;
            tooltipPanel2.SetActive(true);
        }
    }

    public void ShowTooltip3(string price)
    {
        if (tooltipPanel3 != null)
        {

            priceText3.text = price;
            tooltipPanel3.SetActive(true);
        }
    }


    public void HideTooltip()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
            
        }
    }

    public void HideTooltip2()
    {
        if (tooltipPanel2 != null)
        {

            tooltipPanel2.SetActive(false);

        }
    }

    public void HideTooltip3()
    {
        if (tooltipPanel3 != null)
        {

            tooltipPanel3.SetActive(false);
        }
    }
}
