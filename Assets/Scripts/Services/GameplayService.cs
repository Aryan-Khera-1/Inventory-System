using UnityEngine;
 
namespace Game.UI
{
    public class GameplayService
    {
        public ShopSO ShopSO { get; private set; }
        public InventorySO InventorySO { get; private set; }
        
        public GameplayService()
        {
            Initialize();
        }

        public void Initialize()
        {
            ShopSO = UnityEngine.Resources.Load<ShopSO>("Shop/ShopSO");
            InventorySO = UnityEngine.Resources.Load<InventorySO>("Inventory/InventorySO");

            if (ShopSO == null)
                Debug.LogError("ShopSO not found in Shop/ShopSO");
            if (InventorySO == null)
                Debug.LogError("InventorySO not found in Inventory/InventorySO");
        }
    }
}