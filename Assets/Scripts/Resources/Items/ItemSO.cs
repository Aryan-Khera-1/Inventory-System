using Resources.Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int cost;
    public int weight;
    public ItemCategory category;
}