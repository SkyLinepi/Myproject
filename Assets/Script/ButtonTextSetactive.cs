
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextSetactive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DIVE;
    public GameObject OnOfShopAndPlayButton; // ใส่ OnOf ใน Inspector

    public GameObject OnofShowmoney;

    public GameObject OnofSellButton;

    public AudioClip audioClip;
    public AudioSource audioSource;

    public GameObject OnofButton;

    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DIVE.SetActive(true);
    }

    // When mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        //ซ่อนปุ่ม DIVE
        DIVE.SetActive(false);
    }

    public void setBool()
    {
        if(audioClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }

        

        STaticBS.GameStarted = true;

        // ซ่อนปุ่ม OnOfShopAndPlayButton เมื่อกดปุ่มนี้
        if (OnOfShopAndPlayButton != null)
        {
            OnOfShopAndPlayButton.SetActive(false);
        }

        if (OnofSellButton != null)
        {
            OnofSellButton.SetActive(false);
        }

        if(OnofShowmoney != null)
        {
            OnofShowmoney.SetActive(false);
        }

        if(OnofButton != null)
        {
            OnofButton.SetActive(false);
        }
        else
        {
            
        }

    }

    

}
