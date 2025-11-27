using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int LineLevel = 1;
    static public int StrengthLevel = 1;
    static public int SpeedLevel = 1;

    static public int calculatePriceLine;
    static public int calculatePriceStrength = 10;
    static public int calculatePriceSpeed;

    public void UpgardeLine()
    {
        calculatePriceLine += LineLevel*50;
        LineLevel++;
        Debug.Log(LineLevel);
    }
    public void UpgardeStrength()
    {
        calculatePriceStrength += StrengthLevel*50;
        Debug.Log(calculatePriceStrength);
        StrengthLevel++;
        Debug.Log(StrengthLevel);
    }
    public void UpgardeSpeed()
    {
        calculatePriceSpeed += SpeedLevel*50;
        SpeedLevel++;
        Debug.Log(SpeedLevel);
    }
}
