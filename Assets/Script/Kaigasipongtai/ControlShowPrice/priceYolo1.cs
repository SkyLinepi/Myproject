using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class priceYolo1 : MonoBehaviour
{
    public Text priceText;
    void FixedUpdate()
    {
        priceText.text = Shop.calculatePriceStrength.ToString();
    }
}
