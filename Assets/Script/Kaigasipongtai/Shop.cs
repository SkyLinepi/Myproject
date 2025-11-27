using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int LineLevel = 1;
    static public int StrengthLevel = 1;
    static public int SpeedLevel = 1;

    static public int calculatePriceLine;
    static public int calculatePriceStrength;
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
        StrengthLevel++;
        Debug.Log(StrengthLevel);
    }
    public void UpgardeSpeed()
    {
        calculatePriceLine += SpeedLevel*50;
        SpeedLevel++;
        Debug.Log(SpeedLevel);
    }
}
