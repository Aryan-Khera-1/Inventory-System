using UnityEngine;
 
namespace Game.UI
{
    public class GameplayService
    {
        public GridItemSO ShopSo { get; private set; }
        public GridItemSO InventorySo { get; private set; }
        
        public GameplayService()
        {
            Initialize();
        }

        public void Initialize()
        {
            ShopSo = UnityEngine.Resources.Load<GridItemSO>("Shop/ShopSO");
            InventorySo = UnityEngine.Resources.Load<GridItemSO>("Inventory/InventorySO");

            if (ShopSo == null)
                Debug.LogError("ShopSO not found in Shop/ShopSO");
            if (InventorySo == null)
                Debug.LogError("InventorySO not found in Inventory/InventorySO");
        }
    }
}