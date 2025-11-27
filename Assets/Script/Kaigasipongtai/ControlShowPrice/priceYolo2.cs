using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class priceYolo2 : MonoBehaviour
{
    public Text priceText;
    void FixedUpdate()
    {
        priceText.text = Shop.calculatePriceLine.ToString();
    }
}
