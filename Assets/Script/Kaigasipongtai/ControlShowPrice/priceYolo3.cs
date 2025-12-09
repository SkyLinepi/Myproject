using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class priceYolo3 : MonoBehaviour
{
    public Text priceText;
    void FixedUpdate()
    {
        priceText.text = Shop.calculatePriceForce.ToString();
    }
}
