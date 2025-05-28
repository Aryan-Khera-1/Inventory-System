using UnityEngine;

namespace Game.UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Main UI:")]
        private MainUIController mainController;
        [SerializeField] private MainUIView mainView;

        [Header("Inventory UI:")]
        private InventoryUIController inventoryController;
        [SerializeField] private InventoryUIView inventoryView;

        [Header("Shop UI:")]
        private ShopUIController shopController;
        [SerializeField] private ShopUIView shopView;

        [Header("Description UI:")]
        private DescriptionUIController descriptionController;
        [SerializeField] private DescriptionUIView descriptionView;

        private void Start()
        {
            //mainController = new MainUIController(mainView);
            //inventoryController = new InventoryUIController(inventoryView, itemSlotPrefab);
            //shopController = new ShopUIController(shopView, shopItemSlotPrefab);
            //descriptionController = new DescriptionUIController(descriptionView);
        }

        public void Init()
        {
            //
        }
    }
}
