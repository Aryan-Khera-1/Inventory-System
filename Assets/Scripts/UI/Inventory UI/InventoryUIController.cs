namespace Game.UI
{
    public class InventoryUIController
    {
        private InventoryUIView inventoryUIView;

        public InventoryUIController(InventoryUIView inventoryUIView, GameplayService gameplayService, EventService eventService)
        {
            this.inventoryUIView = inventoryUIView;
        }

        public void Show() => inventoryUIView.EnableView();
        public void Hide() => inventoryUIView.DisableView();
    }
}