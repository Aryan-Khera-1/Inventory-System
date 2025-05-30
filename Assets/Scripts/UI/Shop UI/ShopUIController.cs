using Game.UI;

public class ShopUIController
{
    private readonly ShopSO shopData;
    private readonly ShopUIView shopUIView;

    public ShopUIController(ShopUIView shopUIView, GameplayService gameplayService, EventService eventService)
    {
        shopData = gameplayService.ShopSO;
        this.shopUIView = shopUIView;
    }

    public void InitializeShop()
    {
        var itemSlots = shopUIView.GetItemSlots();
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < shopData.items.Count)
            {
                var data = shopData.items[i];
                var slot = itemSlots[i];
                slot.SetItem(data.itemSO.icon, data.quantityAvailable);
            }
            else
            {
                itemSlots[i].SetItem(null, 0);
            }
        }
    }
    
    public void Show() => shopUIView.EnableView();
    public void Hide() => shopUIView.DisableView();
}