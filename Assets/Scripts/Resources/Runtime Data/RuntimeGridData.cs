using System.Collections.Generic;

namespace Resources.Runtime_Data
{
    public class RuntimeGridData
    {
        public List<RuntimeItemData> items = new List<RuntimeItemData>();

        public RuntimeGridData(GridItemSO config)
        {
            foreach (var configItem in config.items)
            {
                items.Add(new RuntimeItemData
                {
                    itemSO = configItem.itemSO,
                    quantity = configItem.quantity
                });
            }
        }
    }
}