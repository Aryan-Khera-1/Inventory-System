public class MainUIController
{
    private MainUIView mainUIView;
    private EventService eventService;

    public MainUIController(MainUIView mainUIView, EventService eventService)
    {
        this.mainUIView = mainUIView;
        this.eventService = eventService;

        Initialize();
    }

    private void Initialize()
    {
        mainUIView.InventoryButton.onClick.AddListener(() => eventService.OnInventoryButtonClicked.InvokeEvent());
        mainUIView.ShopButton.onClick.AddListener(() => eventService.OnShopButtonClicked.InvokeEvent());
    }
    
    public void Show() => mainUIView.EnableView();
    public void Hide() => mainUIView.DisableView();
}