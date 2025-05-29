using UnityEngine;

namespace Game.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Services:")]
        private EventService eventService;
        private GameplayService gameplayService;
        
        [Header("Main UI:")]
        private MainUIController mainController;
        [SerializeField] private MainUIView mainView;

        [Header("Inventory UI:")]
        private InventoryUIController inventoryController;
        [SerializeField] private InventoryUIView inventoryView;

        [Header("Shop UI:")]
        private ShopUIController shopController;
        private ShopSO shopSO;
        [SerializeField] private ShopUIView shopUIView;

        [Header("Description UI:")]
        private DescriptionUIController descriptionController;
        [SerializeField] private DescriptionUIView descriptionView;
        
        private void Start()
        {
            //mainController = new MainUIController(mainView);
            //inventoryController = new InventoryUIController(inventoryView, itemSlotPrefab);
            
            //descriptionController = new DescriptionUIController(descriptionView);
        }

        public void Initialize(EventService eventService, GameplayService gameplayService)
        {
            this.eventService = eventService;
            this.gameplayService = gameplayService;

            shopController = new ShopUIController(gameplayService, shopUIView);
            shopController.InitializeShop();
        }
    }
}
