using Resources.Items;
using Resources.Runtime_Data;
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
            itemGridUIController = new ItemGridUIController(itemGridUIView, gameplayService, eventService, this);
            descriptionController = new DescriptionUIController(descriptionUIView);
            
            SubscribeToEvents();
        }
        
        private void SubscribeToEvents()
        {
            eventService.OnShopButtonClicked.AddListener(ShowItemGridUI);
            eventService.OnInventoryButtonClicked.AddListener(ShowItemGridUI);
            eventService.OnItemClicked.AddListener(OnItemClicked);
            eventService.OnGetItemsButtonClicked.AddListener(OnGetItemsClicked); 
            eventService.OnBuyButtonClicked.AddListener(OnBuyButtonClicked);
            eventService.OnSellButtonClicked.AddListener(OnSellButtonClicked);
        }

        private void OnSellButtonClicked(ItemSO selectedItem, int quantity)
        {
            itemGridUIController.OnSellButtonClicked(selectedItem, quantity);
        }

        private void OnBuyButtonClicked(ItemSO selectedItem, int quantity)
        {
            itemGridUIController.OnBuyButtonClicked(selectedItem, quantity);
        }

        private void OnGetItemsClicked()
        {
            itemGridUIController.OnGetItemClicked();
        }

        private void ShowItemGridUI(RuntimeGridData itemData, string title)
        {
            descriptionController.Hide();
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
        
        public ItemSO SelectedItem => descriptionController.CurrentItem;
        public int SelectedQuantity => descriptionController.SelectedQuantity;
        public bool IsItemSelected => descriptionController.IsActive;

    }
}
