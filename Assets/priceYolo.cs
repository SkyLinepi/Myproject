using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class priceYolo : MonoBehaviour
{
    public Text priceText;
    void FixedUpdate()
    {
        priceText.text = Shop.calculatePriceStrength.ToString();
    }
}
