using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int LineLevel = 2;
    static public int PullLevel = 1;
    static public int SpeedLevel = 2;
    static int count = 30;
    public void UpgardeLine()
    {
        float calculatePrice =  LineLevel *2f ;
        LineLevel++;
        Debug.Log(LineLevel);
    }
    public void UpgardeStrength()
    {
        float calculatePrice =  PullLevel* 1.5f;
        PullLevel++;
        Debug.Log(PullLevel);
    }
    public void UpgardeSpeed()
    {
        float calculatePrice =  SpeedLevel * 0.5f;
        SpeedLevel++;
        
        Debug.Log(SpeedLevel);
    }
}
