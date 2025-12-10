using UnityEngine;
using UnityEngine.UI; // สำหรับ Text Legacy

public class MoneyDisplay : MonoBehaviour
{
    public Text moneyText;
    public GameManager gameManager;
    public GameObject moneyDisplayPanel;
    public AudioClip _audiocilp;
    public AudioSource _audiosorce;
    public void UpdateMoneyUI()
    {
        if (gameManager == null || moneyText == null) return;

        int currentMoney = gameManager.Money;


        moneyText.text = currentMoney.ToString("N0");
    }


    public void ToggleMoneyDisplay()
    {
        if (moneyDisplayPanel == null) return;
        bool isActive = moneyDisplayPanel.activeSelf;
        moneyDisplayPanel.SetActive(!isActive);
        _audiosorce.PlayOneShot(_audiocilp);


        if (!isActive)
        {
            UpdateMoneyUI();
        }
        
    }

    void Start()
    {

        if (moneyDisplayPanel != null && moneyDisplayPanel.activeSelf)
        {
            UpdateMoneyUI();
        }
    }

    public void SellAllfish()
    {

    }


}