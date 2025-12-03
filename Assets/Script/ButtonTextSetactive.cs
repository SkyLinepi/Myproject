
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextSetactive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DIVE;
    
    
    public GameObject OnOfShopAndPlayButton; // ใส่ OnOf ใน Inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        DIVE.SetActive(true);
    }

    // When mouse leaves the button
    public void OnPointerExit(PointerEventData eventData)
    {
        DIVE.SetActive(false);
    }

    public void setBool()
    {
        STaticBS.GameStarted = true;
        
        // ซ่อนปุ่ม OnOfShopAndPlayButton เมื่อกดปุ่มนี้
        if (OnOfShopAndPlayButton != null)
        {
            OnOfShopAndPlayButton.SetActive(false);
        }
    }
}
