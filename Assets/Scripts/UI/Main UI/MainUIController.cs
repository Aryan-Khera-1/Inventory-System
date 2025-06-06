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
        this.gameplayService = gameplayService;
        
        inventoryItemData = gameplayService.InventoryData;
        shopItemData = gameplayService.ShopData;
        
        UpdateCurrencyAndWeight();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        mainUIView.InventoryButton.onClick.AddListener(() => eventService.OnInventoryButtonClicked.InvokeEvent(inventoryItemData, "Inventory"));
        mainUIView.ShopButton.onClick.AddListener(() => eventService.OnShopButtonClicked.InvokeEvent(shopItemData, "Shop"));
        eventService.OnStatsChanged.AddListener(UpdateCurrencyAndWeight);
    }
    
    private void UpdateCurrencyAndWeight()
    {
        var currentCurrency = gameplayService.CurrentCurrency;
        var currentWeight = gameplayService.GetCurrentInventoryWeight();
        var maxWeight = gameplayService.MaxInventoryWeight;

        mainUIView.CurrencyText.text = $"{currentCurrency}";
        mainUIView.WeightText.text = $"{currentWeight}/{maxWeight}";
    }
    
    public void Show() => mainUIView.EnableView();
    public void Hide() => mainUIView.DisableView();
}