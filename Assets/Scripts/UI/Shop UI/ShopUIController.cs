using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIController
{
    private readonly ShopSO shopData;
    private readonly ShopUIView shopView;

    public ShopUIController(ShopSO data, ShopUIView view)
    {
        shopData = data;
        shopView = view;
    }

    public void InitializeShop()
    {
        var itemSlots = shopView.GetItemSlots();
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (i < shopData.items.Count)
            {
                var data = shopData.items[i];
                var slot = itemSlots[i];
                slot.SetItem(data.itemSO.icon, data.quantityAvailable);
            }
            else
            {
                itemSlots[i].SetItem(null, 0);
            }
        }
    }
}
