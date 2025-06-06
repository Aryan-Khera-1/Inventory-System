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
    private readonly UIService uiService;
    private readonly List<ItemSlotUIController> itemSlotControllers;
    
    public ItemGridUIController(ItemGridUIView itemGridUIView, GameplayService gameplayService, EventService eventService, UIService uiService)
    {
        this.itemGridUIView = itemGridUIView;
        this.eventService = eventService;
        this.gameplayService = gameplayService;
        this.uiService = uiService;
        
        itemGridUIView.Initialize(this, eventService, uiService);
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

        for (int i = 1; i <= 3; i++)
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
    
    public void OnBuyButtonClicked(ItemSO selectedItem, int quantity)
    {
        if (!uiService.IsItemSelected)
        {
            Debug.Log("No item selected.");
            return;
        }

        if (!CanBuy(selectedItem, quantity))
        {
            Debug.Log("Not enough currency or inventory space.");
            return;
        }

        PerformBuy(selectedItem, quantity);    }
    
    public void OnSellButtonClicked(ItemSO selectedItem, int quantity)
    {
        if (!uiService.IsItemSelected)
        {
            Debug.Log("No item selected.");
            return;
        }

        if (!CanSell(selectedItem, quantity))
        {
            Debug.Log("Not enough item quantity in inventory.");
            return;
        }

        PerformSell(selectedItem, quantity);
    }

    private bool CanBuy(ItemSO item, int quantity)
    {
        int totalCost = item.cost * quantity;
        int totalWeight = item.weight * quantity;

        return gameplayService.CurrentCurrency >= totalCost &&
               (gameplayService.GetCurrentInventoryWeight() + totalWeight) <= gameplayService.MaxInventoryWeight;
    }

    private void PerformBuy(ItemSO item, int quantity)
    {
        gameplayService.CurrentCurrency -= item.cost * quantity;
        RemoveItem(gameplayService.ShopData.items, item, quantity);
        AddItem(gameplayService.InventoryData.items, item, quantity);
        SetData(gameplayService.ShopData);
        gameplayService.NotifyStatsChanged();
    }

    private bool CanSell(ItemSO item, int quantity)
    {
        var existingItem = gameplayService.InventoryData.items.Find(x => x.itemSO == item);
        return existingItem != null && existingItem.quantity >= quantity;
    }

    private void PerformSell(ItemSO item, int quantity)
    {
        gameplayService.CurrentCurrency += item.cost * quantity;
        RemoveItem(gameplayService.InventoryData.items, item, quantity);
        AddItem(gameplayService.ShopData.items, item, quantity);
        SetData(gameplayService.InventoryData);
        gameplayService.NotifyStatsChanged();
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