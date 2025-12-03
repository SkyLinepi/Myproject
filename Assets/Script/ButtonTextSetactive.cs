
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTextSetactive : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject DIVE;

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
    }
}
