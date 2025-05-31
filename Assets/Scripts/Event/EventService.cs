public class EventService
{
    public EventController OnInventoryButtonClicked { get; private set; }
    public EventController OnShopButtonClicked{get; private set;}
    public EventController<ItemSO, int> OnItemClicked { get; private set; } 


    public EventService()
    {
        OnInventoryButtonClicked = new EventController();
        OnShopButtonClicked = new EventController();
        OnItemClicked = new EventController<ItemSO, int>();
    }
}