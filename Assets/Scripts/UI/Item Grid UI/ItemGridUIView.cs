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
    private UIService uiService;
    
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

    public void Initialize(ItemGridUIController controller, EventService eventService, UIService uiService)
    {
        this.itemGridUIController = controller;
        this.eventService = eventService;
        this.uiService = uiService;
            
        categoriesDropdown.value = 0;
        categoriesDropdown.RefreshShownValue();

        AddListeners();
    }

    private void AddListeners()
    {
        categoriesDropdown.onValueChanged.AddListener(itemGridUIController.OnCategoryDropdownValueChanged);
        getItemButton.onClick.AddListener(OnGetItemButtonClicked);    
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        sellButton.onClick.AddListener(OnSellButtonClicked);
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
    private void OnBuyButtonClicked()
    {
        var item = uiService.SelectedItem;
        var quantity = uiService.SelectedQuantity;
        
        if (item == null)
        {
            Debug.LogWarning("No item selected for Buy.");
            return;
        }

        eventService.OnBuyButtonClicked.InvokeEvent(item, quantity);
    }

    private void OnSellButtonClicked()
    {
        var item = uiService.SelectedItem;
        var quantity = uiService.SelectedQuantity;

        if (item == null)
        {
            Debug.LogWarning("No item selected for Sell.");
            return;
        }

        eventService.OnSellButtonClicked.InvokeEvent(item, quantity);
    }

    
    public void EnableView() => gameObject.SetActive(true);
    public void DisableView() => gameObject.SetActive(false);
}
