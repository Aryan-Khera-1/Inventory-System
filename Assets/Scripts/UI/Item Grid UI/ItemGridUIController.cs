using System.Collections.Generic;
using Game.UI;
using Resources.Items;
using UnityEngine;

public class ItemGridUIController
{
    private GridItemSO currentItemData;
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

    public void SetData(GridItemSO gridData)
    {
        currentItemData = gridData;
        ResetItems(gridData.items);
    }

    public void OnCategoryDropdownValueChanged(int selectedIndex)
    {
        var selectedCategory = (ItemCategory)selectedIndex;
        var filteredItems = new List<GridItemData>();

        foreach (var itemData in currentItemData.items)
        {
            if (selectedCategory == ItemCategory.All || itemData.itemSO.category == selectedCategory)
            {
                filteredItems.Add(itemData);
            }
        }

        ResetItems(filteredItems);
    }
    
    public void AddItem(GridItemData newItem)
    {
        var existingItem = currentItemData.items.Find(x => x.itemSO == newItem.itemSO);
        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity;
        }
        else
        {
            currentItemData.items.Add(newItem);
        }

        RefreshCurrentView();
    }

    public void RemoveItem(GridItemData itemToRemove)
    {
        var existingItem = currentItemData.items.Find(x => x.itemSO == itemToRemove.itemSO);
        if (existingItem != null)
        {
            existingItem.quantity -= itemToRemove.quantity;
            if (existingItem.quantity <= 0)
            {
                currentItemData.items.Remove(existingItem);
            }
        }

        RefreshCurrentView();
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


    public void ResetItems(List<GridItemData> items)
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
        var shopItems = gameplayService.ShopSo.items;
        var inventoryItems = gameplayService.InventorySo.items;

        if (shopItems.Count == 0)
        {
            Debug.Log("No items in shop.");
            return;
        }

        var random = new System.Random();

        for (int i = 0; i < 3; i++)
        {
            // Pick random item
            var randomIndex = UnityEngine.Random.Range(0, shopItems.Count);
            var shopItem = shopItems[randomIndex];

            // If item has no stock, skip
            if (shopItem.quantity <= 0)
            {
                i--;
                continue;
            }

            // Random quantity (between 1 and shop stock)
            var quantity = random.Next(1, shopItem.quantity + 1);

            // Deduct from shop
            shopItem.quantity -= quantity;

            // Add to inventory
            var existingInventoryItem = inventoryItems.Find(x => x.itemSO == shopItem.itemSO);
            if (existingInventoryItem != null)
            {
                existingInventoryItem.quantity += quantity;
            }
            else
            {
                inventoryItems.Add(new GridItemData
                {
                    itemSO = shopItem.itemSO,
                    quantity = quantity
                });
            }
        }

        // Refresh the inventory view
        SetData(gameplayService.InventorySo);
        Debug.Log("3 random items picked from shop and added to inventory.");
    }
    
    public void Show() => itemGridUIView.EnableView();
    public void Hide() => itemGridUIView.DisableView();

}