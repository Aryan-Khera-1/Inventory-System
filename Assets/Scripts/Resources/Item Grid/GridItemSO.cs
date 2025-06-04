using System.Collections;
using System.Collections.Generic;
using Resources.Items;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GridData")]
public class GridItemSO : ScriptableObject
{
    public List<GridItemData> items;
}
