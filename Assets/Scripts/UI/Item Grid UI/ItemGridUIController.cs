using System.Collections.Generic;
using Game.UI;
using Resources.Items;
using Resources.Runtime_Data;
using UnityEngine;

public class ItemGridUIController
{
    private RuntimeGridData currentItemData;
    private readonly ItemGridUIView itemGridUIView;
    private readonly EventService eventService;
    private readonly GameplayService gameplayService;
    private readonly List<ItemSlotUIController> itemSlotControllers;
    
    public ItemGridUIController(ItemGridUIView itemGridUIView, GameplayService gameplayService, EventService eventService)
    {
        this.itemGridUIView = itemGridUIView;
        this.eventService = eventService;
        this.gameplayService = gameplayService;
        
        itemGridUIView.Initialize(this, eventService);
        itemSlotControllers = new List<ItemSlotUIController>();
        InitializeItemSlotControllers();
    }

    
    private void InitializeItemSlotControllers()
    {
        var itemSlotUIViews = itemGridUIView.GetItemSlots();
        foreach (var view in itemSlotUIViews)
        {
            var controller = new ItemSlotUIController(view, eventService);
            itemSlotControllers.Add(controller);
        }
    }

    public void SetData(RuntimeGridData gridData)
    {
        currentItemData = gridData;
        ResetItems(gridData.items);
    }

    public void OnCategoryDropdownValueChanged(int selectedIndex)
    {
        var selectedCategory = (ItemCategory)selectedIndex;
        var filteredItems = new List<RuntimeItemData>();

        foreach (var itemData in currentItemData.items)
        {
            if (selectedCategory == ItemCategory.All || itemData.itemSO.category == selectedCategory)
            {
                filteredItems.Add(itemData);
            }
        }

        ResetItems(filteredItems);
    }

    private void RefreshCurrentView()
    {
        ResetItems(currentItemData.items);
    }

    private void RefreshFilteredView()
    {
        var selectedCategoryIndex = itemGridUIView.CategoriesDropdown.value;
        OnCategoryDropdownValueChanged(selectedCategoryIndex);
    }


    public void ResetItems(List<RuntimeItemData> items)
    {
        for (int i = 0; i < itemSlotControllers.Count; i++)
        {
            if (i < items.Count)
            {
                var data = items[i];
                itemSlotControllers[i].SetItem(data.quantity, data.itemSO);
            }
            else
            {
                itemSlotControllers[i].SetItem(0);
            }
        }
    }
    
    public void OnGetItemClicked()
    {
        var shopItems = gameplayService.ShopData.items;
        var inventoryItems = gameplayService.InventoryData.items;

        if (shopItems.Count == 0)
        {
            Debug.Log("No items in shop.");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            var shopItem = GetRandomAvailableItem(shopItems);
            if (shopItem == null)
            {
                i--;
                continue;
            }

            var quantity = GetRandomQuantity(shopItem.quantity);

            RemoveItem(shopItems, shopItem.itemSO, quantity);
            AddItem(inventoryItems, shopItem.itemSO, quantity);
        }

        SetData(gameplayService.InventoryData);
        Debug.Log("3 random items picked from shop and added to inventory.");
    }
    
    public void OnBuyButtonClicked()
    {
        throw new System.NotImplementedException();
    }
    
    public void OnSellButtonClicked()
    {
        throw new System.NotImplementedException();
    }

    private RuntimeItemData GetRandomAvailableItem(List<RuntimeItemData> items)
    {
        var availableItems = items.FindAll(x => x.quantity > 0);
        if (availableItems.Count == 0)
            return null;

        var randomIndex = UnityEngine.Random.Range(0, availableItems.Count);
        return availableItems[randomIndex];
    }

    private int GetRandomQuantity(int max)
    {
        var random = new System.Random();
        return random.Next(1, max + 1);
    }

    private void AddItem(List<RuntimeItemData> targetList, ItemSO itemSO, int quantity)
    {
        var existingItem = targetList.Find(x => x.itemSO == itemSO);
        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            targetList.Add(new RuntimeItemData
            {
                itemSO = itemSO,
                quantity = quantity
            });
        }
    }

    private void RemoveItem(List<RuntimeItemData> sourceList, ItemSO itemSO, int quantity)
    {
        var existingItem = sourceList.Find(x => x.itemSO == itemSO);
        if (existingItem != null)
        {
            existingItem.quantity -= quantity;
            if (existingItem.quantity <= 0)
            {
                sourceList.Remove(existingItem);
            }
        }
    }

    
    
    
    public void Show() => itemGridUIView.EnableView();
    public void Hide() => itemGridUIView.DisableView();

}