using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    
    static public int reloadTimeLevel = 2;
    static public int StrengthLevel = 2;
    static public int SpeedLevel = 2;

    static public int calculatePriceReloadTime = 2;
    static public int calculatePriceStrength = 10;
    static public int calculatePriceForce = 4;

    public Player _reload;
    public Player _force;
    public GameManager _pullStrenght;

    public void Start()
    {

    }
    public void PriceforUpgardeReloadTime()
    {
        calculatePriceReloadTime += reloadTimeLevel*20;
        Debug.Log(calculatePriceReloadTime); 
        reloadTimeLevel++;  
        Debug.Log(reloadTimeLevel);
    }
    public void PriceforUpgardeStrength()
    {
        calculatePriceStrength += StrengthLevel*15;
        Debug.Log(calculatePriceStrength);
        StrengthLevel++;
        Debug.Log(StrengthLevel);
    }
    public void PriceforUpgradeForce()
    {
        calculatePriceForce += SpeedLevel * 2;
        SpeedLevel++;
        Debug.Log("Level Speed now =  " + SpeedLevel);
    }

    public void UpdateReload()
    {
        _reload.reloadTime -= 0.45f;
        Debug.Log("Time to reload = " +_reload.reloadTime);
    }

    public void UpdatepullStrenght()
    {
        _pullStrenght.PullStrength += 0.25f;
        Debug.Log("Strenght for Pull =" + _pullStrenght.PullStrength);
    }

    public void UpdateForce()
    {
        _force.force += 20f;
        _force.collected = _force.force;
        Debug.Log("Speed for Move =  " + _force.force );
    }

}
