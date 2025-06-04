
using Resources.Items;

public class EventService
{
    public EventController<GridItemSO, string> OnInventoryButtonClicked { get; private set; }
    public EventController<GridItemSO, string> OnShopButtonClicked{get; private set;}
    public EventController<ItemSO, int> OnItemClicked { get; private set; } 

    public EventController<ItemCategory> OnCategoryChanged {get; private set;}



    public EventService()
    {
        OnInventoryButtonClicked = new EventController<GridItemSO, string>();
        OnShopButtonClicked = new EventController<GridItemSO, string>();
        OnItemClicked = new EventController<ItemSO, int>();
        OnCategoryChanged = new EventController<ItemCategory>();
    }
}