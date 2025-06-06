using UnityEngine;

namespace Game.UI
{
    public class DescriptionUIController
    {
        private DescriptionUIView descriptionUIView;
        private ItemSO currentItem;
        private int selectedQuantity = 1;
        
        public int SelectedQuantity => selectedQuantity;
        public ItemSO CurrentItem => currentItem;
        public bool IsActive => descriptionUIView.IsActive;



        public DescriptionUIController(DescriptionUIView descriptionUIView)
        {
            this.descriptionUIView = descriptionUIView;
            descriptionUIView.QuantitySlider.onValueChanged.AddListener(OnQuantitySliderChanged);
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