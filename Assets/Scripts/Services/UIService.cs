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

        public void Initialize(EventService eventService, GameplayService gameplayService)
        {
            this.eventService = eventService;
            this.gameplayService = gameplayService;
            
            mainController = new MainUIController(mainUIView, eventService);
            shopController = new ShopUIController(shopUIView, gameplayService, eventService);
            inventoryController = new InventoryUIController(inventoryUIView, gameplayService, eventService);;
            descriptionController = new DescriptionUIController(descriptionUIView);

            shopController.InitializeShop();
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            eventService.OnInventoryButtonClicked.AddListener(ShowInventoryUI);
            eventService.OnShopButtonClicked.AddListener(ShowShopUI);
            eventService.OnItemClicked.AddListener(OnItemClicked);
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
        
        private void OnItemClicked(ItemSO item, int quantityAvailable)
        {
            descriptionController.SetItemData(item, quantityAvailable);
            descriptionController.Show();
        }
    }
}
