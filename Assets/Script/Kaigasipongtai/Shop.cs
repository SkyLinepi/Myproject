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
        FishingLine2D.maxLineLength = 5f * LineLevel;
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
<<<<<<< Updated upstream
        Debug.Log(SpeedLevel);
=======
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
            gameManager.PullStrength += 1.5f;
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
>>>>>>> Stashed changes
    }

}
