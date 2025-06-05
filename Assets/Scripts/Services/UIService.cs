using Resources.Items;
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

        [Header("Item Grid UI:")]
        private ItemGridUIController itemGridUIController;
        [SerializeField] private ItemGridUIView itemGridUIView;

        [Header("Description UI:")]
        private DescriptionUIController descriptionController;
        [SerializeField] private DescriptionUIView descriptionUIView;

        public void Initialize(EventService eventService, GameplayService gameplayService)
        {
            this.eventService = eventService;
            this.gameplayService = gameplayService;
            
            mainController = new MainUIController(mainUIView, eventService, gameplayService);
            itemGridUIController = new ItemGridUIController(itemGridUIView, gameplayService, eventService);
            descriptionController = new DescriptionUIController(descriptionUIView);
            
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            eventService.OnShopButtonClicked.AddListener(ShowItemGridUI);
            eventService.OnInventoryButtonClicked.AddListener(ShowItemGridUI);
            eventService.OnItemClicked.AddListener(OnItemClicked);
            eventService.OnGetItemsButtonClicked.AddListener(OnGetItemsClicked);
        }

        private void OnGetItemsClicked()
        {
            itemGridUIController.OnGetItemClicked();
        }

        private void ShowItemGridUI(GridItemSO itemData, string title)
        {
            itemGridUIController.SetData(itemData);
            itemGridUIView.SetTitle(title);
            itemGridUIView.SetupButtons(title);
            itemGridUIController.Show();
        }
        
        private void OnItemClicked(ItemSO item, int quantityAvailable)
        {
            descriptionController.SetItemData(item, quantityAvailable);
            descriptionController.Show();
        }
    }
}
