using Resources.Runtime_Data;
using UnityEngine;
 
namespace Game.UI
{
    public class GameplayService
    {
        [Header("Configs")]
        public GridItemSO ShopSo;
        public GridItemSO InventorySo;
        
        [field: Header("Runtime Data")]
        public RuntimeGridData ShopData { get; private set; }
        public RuntimeGridData InventoryData { get; private set; }
        
        public int CurrentCurrency { get; set; } = 1000;
        public int MaxInventoryWeight { get; private set; } = 500;

        public int GetCurrentInventoryWeight()
        {
            int totalWeight = 0;
            foreach (var item in InventoryData.items)
            {
                totalWeight += item.itemSO.weight * item.quantity;
            }
            return totalWeight;
        }

        public GameplayService()
        {
            Initialize();
        }

        public void Initialize()
        {
            ShopSo = UnityEngine.Resources.Load<GridItemSO>("Shop/ShopSO");
            InventorySo = UnityEngine.Resources.Load<GridItemSO>("Inventory/InventorySO");
            
            ShopData = new RuntimeGridData(ShopSo);
            InventoryData = new RuntimeGridData(InventorySo);

            if (ShopSo == null)
                Debug.LogError("ShopSO not found in Shop/ShopSO");
            if (InventorySo == null)
                Debug.LogError("InventorySO not found in Inventory/InventorySO");
        }
    }
}