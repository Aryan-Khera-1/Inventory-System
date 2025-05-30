using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Game.UI;

public class ShopUIView : MonoBehaviour, IUIView
{
    [Header("UI References")]
    [SerializeField] private Transform itemSlotsContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button BuyButton => buyButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;


    public List<ItemSlotUIView> GetItemSlots()
    {
        List<ItemSlotUIView> itemSlotViews = new List<ItemSlotUIView>();
        foreach (Transform child in itemSlotsContainer)
        {
            var slot = child.GetComponent<ItemSlotUIView>();
            if (slot != null)
                itemSlotViews.Add(slot);
        }
        return itemSlotViews;
    }

    public void EnableView() => gameObject.SetActive(true);
    public void DisableView() => gameObject.SetActive(false);
}
