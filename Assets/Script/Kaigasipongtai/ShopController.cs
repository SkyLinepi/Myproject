using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private GameObject SubMenuContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TriggerSubMenu()
    {
        bool isActive = SubMenuContainer.activeSelf;
        SubMenuContainer.SetActive(!isActive);
    }
}
