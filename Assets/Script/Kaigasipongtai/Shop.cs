using System;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{

    static public int reloadTimeLevel = 1;
    static public int StrengthLevel = 1;
    static public int SpeedLevel = 1;

    static public int calculatePriceReloadTime = 2;
    static public int calculatePriceStrength = 10;
    static public int calculatePriceForce = 4;

    public Player _player;
    public GameManager gameManager;
    public AudioSource _audiosource;
    public AudioClip _audioClip;
    

    public void Start()
    {
        
    }
    public void PriceforUpgardeReloadTime()
    {
        if (gameManager.Money >= calculatePriceReloadTime)
        {
            calculatePriceReloadTime += reloadTimeLevel * 15;
        Debug.Log(calculatePriceReloadTime);
        reloadTimeLevel++;
        Debug.Log(reloadTimeLevel);
        }
        else
        {
            
        }

    }
    public void PriceforUpgardeStrength()
    {
        if(gameManager.Money >= calculatePriceStrength)
        {
            calculatePriceStrength += StrengthLevel * 15;
        Debug.Log(calculatePriceStrength);
        StrengthLevel++;
        Debug.Log(StrengthLevel);
        }
    }
    public void PriceforUpgradeForce()
    {
        if (gameManager.Money>= calculatePriceForce)
        {
            calculatePriceForce += SpeedLevel * 20;
        SpeedLevel++;
        Debug.Log("Level Speed now =  " + SpeedLevel);
        Debug.Log("next for price upgrade = " + calculatePriceForce);
        }
    }

    public void UpdateReload()
    {
        if (gameManager.Money >= calculatePriceReloadTime)
        {
            if (_player.reloadTime >= 0)
            {
                _player.reloadTime -= 0.25f;
                Debug.Log("Time to reload = " + _player.reloadTime);
                gameManager.Money -= calculatePriceReloadTime;
                if (_audiosource != null && _audioClip != null)
                {
                    _audiosource.PlayOneShot(_audioClip);
                }
            }
        }
        else
        {

        }
    }

    public void UpdatepullStrenght()
    {
        if (gameManager.Money >= calculatePriceStrength)
        {
            gameManager.PullStrength += 0.25f;
            Debug.Log("Strenght for Pull =" + gameManager.PullStrength);
            gameManager.Money -= calculatePriceStrength;
            if (_audiosource != null && _audioClip != null)
                {
                    _audiosource.PlayOneShot(_audioClip);
                }
        }
        else
        {

        }

    }

    public void UpdateForce()
    {
        if (gameManager.Money >= calculatePriceForce)
        {
            _player.force += 2f;
            _player.collected = _player.force;
            Debug.Log("Speed for Move =  " + _player.force);
            gameManager.Money -= calculatePriceForce;
            if (_audiosource != null && _audioClip != null)
                {
                    _audiosource.PlayOneShot(_audioClip);
                }
        }
        else
        {

        }
    }

}
