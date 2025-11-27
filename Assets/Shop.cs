using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int LineLevel = 1;
    static public int StrengthLevel = 1;
    static public int SpeedLevel = 1;
    public void UpgardeLine()
    {
        int calculatePrice =  LineLevel*2;
        LineLevel++;
        Debug.Log(LineLevel);
    }
    public void UpgardeStrength()
    {
        int calculatePrice =  StrengthLevel*2;
    }
    public void UpgardeSpeed()
    {
        int calculatePrice =  SpeedLevel*2;
    }
}
