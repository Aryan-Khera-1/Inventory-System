using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<InventoryItemData> items;
}