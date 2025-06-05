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
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button getItemButton;
    [SerializeField] private Dropdown categoriesDropdown;

    public Transform ItemSlotsContainer => itemSlotsContainer;
    public Button BuyButton => buyButton;
    public Button SellButton => sellButton;
    public Button GetItemButton => getItemButton;
    public Dropdown CategoriesDropdown => categoriesDropdown;

    public void Initialize(ItemGridUIController controller, EventService eventService)
    {
        this.itemGridUIController = controller;
        this.eventService = eventService;
            
        categoriesDropdown.value = 0;
        categoriesDropdown.RefreshShownValue();

        AddListeners();
    }

    private void AddListeners()
    {
        categoriesDropdown.onValueChanged.AddListener(itemGridUIController.OnCategoryDropdownValueChanged);
        getItemButton.onClick.AddListener(OnGetItemButtonClicked);    
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
    
    public void SetupButtons(string viewType)
    {
        buyButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
        getItemButton.gameObject.SetActive(false);

        if (viewType == "Shop")
        {
            buyButton.gameObject.SetActive(true);
        }
        else if (viewType == "Inventory")
        {
            sellButton.gameObject.SetActive(true);
            getItemButton.gameObject.SetActive(true);
        }
    }

    private void OnGetItemButtonClicked()
    {
        eventService.OnGetItemsButtonClicked.InvokeEvent();
        getItemButton.interactable = false;
    }
    
    public void EnableView() => gameObject.SetActive(true);
    public void DisableView() => gameObject.SetActive(false);
}
