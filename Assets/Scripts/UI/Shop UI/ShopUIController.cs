using System.Collections.Generic;
using Game.UI;
using Resources.Items;

public class ShopUIController
{
    private readonly ShopSO shopData;
    private readonly ShopUIView shopUIView;
    private readonly EventService eventService;
    private readonly List<ItemSlotUIController> itemSlotControllers;
    
    public ShopUIController(ShopUIView shopUIView, GameplayService gameplayService, EventService eventService)
    {
        shopData = gameplayService.ShopSO;
        this.shopUIView = shopUIView;
        this.eventService = eventService;
        
        shopUIView.Initialize(this, eventService);
        
        itemSlotControllers = new List<ItemSlotUIController>();
        InitializeItemSlotControllers();
    }

    private void InitializeItemSlotControllers()
    {
        var itemSlotUIViews = shopUIView.GetItemSlots();
        foreach (var view in itemSlotUIViews)
        {
            var controller = new ItemSlotUIController(view, eventService);
            itemSlotControllers.Add(controller);
        }
    }

    public void InitializeShop()
    {
        SetItems(shopData.items);
    }

    public void OnCategoryDropdownValueChanged(int selectedIndex)
    {
        var selectedCategory = (ItemCategory)selectedIndex;

        var filteredItems = new List<ShopItemData>();

        foreach (var itemData in shopData.items)
        {
            if (selectedCategory == ItemCategory.All || itemData.itemSO.category == selectedCategory)
            {
                filteredItems.Add(itemData);
            }
        }

        SetItems(filteredItems);
    }

    public void SetItems(List<ShopItemData> items)
    {
        for (int i = 0; i < itemSlotControllers.Count; i++)
        {
            if (i < items.Count)
            {
                var data = items[i];
                itemSlotControllers[i].SetItem(data.quantityAvailable, data.itemSO);
            }
            else
            {
                itemSlotControllers[i].SetItem(0);
            }
        }
    }
    
    public void Show() => shopUIView.EnableView();
    public void Hide() => shopUIView.DisableView();

}