using System;
using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int LineLevel = 1;
    static public int StrengthLevel = 2;
    static public int SpeedLevel = 2;

    static public int calculatePriceLine = 2;
    static public int calculatePriceStrength = 10;
    static public int calculatePriceSpeed = 4;

    public void UpgardeLine()
    {
        calculatePriceLine += LineLevel*20;
        LineLevel++;
        Debug.Log(LineLevel);
    }
    public void UpgardeStrength()
    {
        calculatePriceStrength += StrengthLevel*15;
        Debug.Log(calculatePriceStrength);
        StrengthLevel++;
        Debug.Log(StrengthLevel);
    }
    public void UpgardeSpeed()
    {
        calculatePriceSpeed += SpeedLevel ^ 2;
        SpeedLevel++;
        Debug.Log(SpeedLevel);
    }
}
