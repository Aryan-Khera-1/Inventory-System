using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.UI;
using Resources.Items;
using TMPro;

public class ItemGridUIView : MonoBehaviour, IUIView
{
    private ItemGridUIController itemGridUIController;
    private EventService eventService;
    
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Transform itemSlotsContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button getItemButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button BuyButton => buyButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;

    public void Initialize(ItemGridUIController controller, EventService eventService)
    {
        this.itemGridUIController = controller;
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
    
    public void SetTitle(string titleTextValue)
    {
        titleText.text = titleTextValue;
    }

    public void EnableView() => gameObject.SetActive(true);
    public void DisableView() => gameObject.SetActive(false);
}
