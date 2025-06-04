using System.Collections.Generic;
using Game.UI;
using Resources.Items;

public class ItemGridUIController
{
    private GridItemSO currentItemData;
    private readonly ItemGridUIView itemGridUIView;
    private readonly EventService eventService;
    private readonly List<ItemSlotUIController> itemSlotControllers;
    
    public ItemGridUIController(ItemGridUIView itemGridUIView, GameplayService gameplayService, EventService eventService)
    {
        this.itemGridUIView = itemGridUIView;
        this.eventService = eventService;
        
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
        SetItems(gridData.items);
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

        SetItems(filteredItems);
    }

    public void SetItems(List<GridItemData> items)
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
    
    public void Show() => itemGridUIView.EnableView();
    public void Hide() => itemGridUIView.DisableView();
}