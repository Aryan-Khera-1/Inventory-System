using UnityEngine;

public class ItemSlotUIController
{
    private ItemSlotUIView itemSlotUIView;
    private EventService eventService;
    private ItemSO currentItemSO;
    private int quantityAvailable;


    public ItemSlotUIController(ItemSlotUIView itemSlotUIView, EventService eventService)
    {
        this.itemSlotUIView = itemSlotUIView;
        this.eventService = eventService;
        SubscribeToButton();
    }

    private void SubscribeToButton()
    {
        itemSlotUIView.IconButton.onClick.AddListener(OnIconButtonClicked);
    }

    private void OnIconButtonClicked()
    {
        if (currentItemSO != null)
        {
            eventService.OnItemClicked.InvokeEvent(currentItemSO, quantityAvailable);
        }    
    }
    
    public void SetItem(int quantityAvailable, ItemSO itemSO = null)
    {
        currentItemSO = itemSO;
        this.quantityAvailable = quantityAvailable;
        itemSlotUIView.SetItem(currentItemSO?.icon, quantityAvailable);
    }

    public void ClearItem()
    {
        itemSlotUIView.SetItem(null, 0);
    }
}