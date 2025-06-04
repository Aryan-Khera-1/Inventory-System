using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.UI;
using Resources.Items;

public class ShopUIView : MonoBehaviour, IUIView
{
    private ShopUIController shopUIController;
    private EventService eventService;
    
    [Header("UI References")]
    [SerializeField] private Transform itemSlotsContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button BuyButton => buyButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;

    public void Initialize(ShopUIController controller, EventService eventService)
    {
        shopUIController = controller;
        this.eventService = eventService;
        
        categoriesDropdown.value = 0;
        categoriesDropdown.RefreshShownValue(); ;
        categoriesDropdown.onValueChanged.AddListener(controller.OnCategoryDropdownValueChanged);
    }

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
