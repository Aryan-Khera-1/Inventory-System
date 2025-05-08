using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIView : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform itemSlotsContainer;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button getItemsButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button SellButton => sellButton;
    public Button GetItemsButton => getItemsButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;
}
