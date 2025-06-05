using Game.UI;
using Resources.Runtime_Data;

public class MainUIController
{
    private MainUIView mainUIView;
    private EventService eventService;
    private GameplayService gameplayService;
    private RuntimeGridData inventoryItemData;
    private RuntimeGridData shopItemData;
    
    public MainUIController(MainUIView mainUIView, EventService eventService, GameplayService gameplayService)
    {
        this.mainUIView = mainUIView;
        this.eventService = eventService;
        inventoryItemData = gameplayService.InventoryData;
        shopItemData = gameplayService.ShopData;

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        mainUIView.InventoryButton.onClick.AddListener(() => eventService.OnInventoryButtonClicked.InvokeEvent(inventoryItemData, "Inventory"));
        mainUIView.ShopButton.onClick.AddListener(() => eventService.OnShopButtonClicked.InvokeEvent(shopItemData, "Shop"));
    }
    
    public void Show() => mainUIView.EnableView();
    public void Hide() => mainUIView.DisableView();
}