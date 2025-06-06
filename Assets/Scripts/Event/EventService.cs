
using Resources.Items;
using Resources.Runtime_Data;

public class EventService
{
    public EventController<RuntimeGridData, string> OnInventoryButtonClicked { get; private set; }
    public EventController<RuntimeGridData, string> OnShopButtonClicked{get; private set;}
    public EventController<ItemSO, int> OnItemClicked { get; private set; } 

    public EventController<ItemCategory> OnCategoryChanged {get; private set;}

    public EventController<ItemSO, int> OnBuyButtonClicked { get; private set; }
    public EventController<ItemSO, int> OnSellButtonClicked { get; private set; }
    public EventController OnGetItemsButtonClicked { get; private set; }
    public EventController OnStatsChanged { get; private set; }
    public EventController<ItemSO, int> OnTransactionComplete { get; private set; }


    public EventService()
    {
        OnInventoryButtonClicked = new EventController<RuntimeGridData, string>();
        OnShopButtonClicked = new EventController<RuntimeGridData, string>();
        OnItemClicked = new EventController<ItemSO, int>();
        OnCategoryChanged = new EventController<ItemCategory>();
        OnBuyButtonClicked = new EventController<ItemSO, int>();
        OnSellButtonClicked = new EventController<ItemSO, int>();
        OnGetItemsButtonClicked = new EventController();
        OnStatsChanged = new EventController();
        OnTransactionComplete = new EventController<ItemSO, int>();
    }
}