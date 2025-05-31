using System.Collections.Generic;
using Game.UI;

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
        for (int i = 0; i < itemSlotControllers.Count; i++)
        {
            if (i < shopData.items.Count)
            {
                var data = shopData.items[i];
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