using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform itemSlotsContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button BuyButton => buyButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;
}
