using Game.UI;

public class MainUIController
{
    private MainUIView mainUIView;
    private EventService eventService;
    private GameplayService gameplayService;
    private GridItemSO inventoryItemData;
    private GridItemSO shopItemData;
    
    public MainUIController(MainUIView mainUIView, EventService eventService, GameplayService gameplayService)
    {
        this.mainUIView = mainUIView;
        this.eventService = eventService;
        inventoryItemData = gameplayService.InventorySo;
        shopItemData = gameplayService.ShopSo;

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