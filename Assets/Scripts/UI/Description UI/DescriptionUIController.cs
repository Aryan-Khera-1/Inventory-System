using UnityEngine;

namespace Game.UI
{
    public class DescriptionUIController
    {
        private EventService eventService;
        private GameplayService gameplayService;
        private DescriptionUIView descriptionUIView;
        private bool colorizeDescriptionStats;
        private ItemSO currentItem;
        private int selectedQuantity = 1;
        
        public void SetColorizeDescriptionStats(bool value) => colorizeDescriptionStats = value;
        public int SelectedQuantity => selectedQuantity;
        public ItemSO CurrentItem => currentItem;
        public bool IsActive => descriptionUIView.IsActive;

        public DescriptionUIController(DescriptionUIView descriptionUIView, EventService eventService, GameplayService gameplayService)
        {
            this.descriptionUIView = descriptionUIView;
            this.eventService = eventService;
            this.gameplayService = gameplayService;
            
            descriptionUIView.QuantitySlider.onValueChanged.AddListener(OnQuantitySliderChanged);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            eventService.OnTransactionComplete.AddListener(OnTransactionDone);
        }

        private void OnTransactionDone(ItemSO item, int updatedQuantityAvailable)
        {
            if (!IsActive || currentItem == null || item != currentItem)
                return;

            descriptionUIView.QuantitySlider.maxValue = updatedQuantityAvailable;

            if (updatedQuantityAvailable == 0)
            {
                Hide();
                return;
            }

            descriptionUIView.QuantitySlider.value = 1;
            descriptionUIView.QuantityValueToSet.text = selectedQuantity.ToString();

            UpdateCostAndWeight(selectedQuantity);
        }

        private void OnQuantitySliderChanged(float newValue)
        {
            selectedQuantity = Mathf.RoundToInt(newValue);
            descriptionUIView.QuantityValueToSet.text = selectedQuantity.ToString();
            UpdateCostAndWeight(selectedQuantity);
        }
        
        private void UpdateCostAndWeight(int quantity)
        {
            if (currentItem == null) return;

            int totalCost = currentItem.cost * quantity;
            int totalWeight = currentItem.weight * quantity;

            descriptionUIView.CurrencyRequired.text = totalCost.ToString();
            descriptionUIView.WeightRequired.text = totalWeight.ToString();
            
            HandleStatsColor(totalCost, totalWeight);
        }

        private void HandleStatsColor(int totalCost, int totalWeight)
        {
            int remainingWeightCapacity = gameplayService.MaxInventoryWeight - gameplayService.GetCurrentInventoryWeight();

            if (colorizeDescriptionStats)
            {
                descriptionUIView.SetTextColor(descriptionUIView.CurrencyRequired, totalCost <= gameplayService.CurrentCurrency);
                descriptionUIView.SetTextColor(descriptionUIView.WeightRequired, totalWeight <= remainingWeightCapacity);
            }
            else
            {
                descriptionUIView.ResetTextColor(descriptionUIView.CurrencyRequired);
                descriptionUIView.ResetTextColor(descriptionUIView.WeightRequired);
            }
        }
        public void SetItemData(ItemSO item, int quantityAvailable)
        {
            currentItem = item;
            
            descriptionUIView.ItemName.text = item.itemName;
            descriptionUIView.DescriptionText.text = item.description;
            descriptionUIView.ItemIcon.sprite = item.icon;

            descriptionUIView.QuantitySlider.minValue = 1; 
            descriptionUIView.QuantitySlider.maxValue = quantityAvailable; 
            descriptionUIView.QuantitySlider.value = 1;
            
            UpdateCostAndWeight(1);
        }

        public void Show() => descriptionUIView.EnableView();
        public void Hide() => descriptionUIView.DisableView();
    }
}