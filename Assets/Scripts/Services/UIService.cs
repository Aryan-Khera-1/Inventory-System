using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Services:")]
        private EventService eventService;
        private GameplayService gameplayService;
        
        [Header("Main UI:")]
        private MainUIController mainController;
        [SerializeField] private MainUIView mainUIView;

        [Header("Inventory UI:")]
        private InventoryUIController inventoryController;
        [SerializeField] private InventoryUIView inventoryUIView;

        [Header("Shop UI:")]
        private ShopUIController shopController;
        private ShopSO shopSO;
        [SerializeField] private ShopUIView shopUIView;

        [Header("Description UI:")]
        private DescriptionUIController descriptionController;
        [SerializeField] private DescriptionUIView descriptionUIView;
        
        private void Start()
        {
            //mainController = new MainUIController(mainView);
            
            //descriptionController = new DescriptionUIController(descriptionView);
        }

        public void Initialize(EventService eventService, GameplayService gameplayService)
        {
            this.eventService = eventService;
            this.gameplayService = gameplayService;
            
            mainController = new MainUIController(mainUIView, eventService);

            shopController = new ShopUIController(shopUIView, gameplayService, eventService);
            shopController.InitializeShop();
            
            inventoryController = new InventoryUIController(inventoryUIView, gameplayService, eventService);;

            
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            eventService.OnInventoryButtonClicked.AddListener(ShowInventoryUI);
            eventService.OnShopButtonClicked.AddListener(ShowShopUI);
        }
        
        private void ShowInventoryUI()
        {
            inventoryController.Show();
            shopController.Hide();
        }

        private void ShowShopUI()
        {
            shopController.Show();
            inventoryController.Hide();
        }

    }
}
