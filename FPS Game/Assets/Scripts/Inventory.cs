using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUI InvenUI;
    private readonly Dictionary<ItemData, int> items = new();

    public bool AddItem(ItemData item, int amount = 1)
    {
        if(item == null || amount <= 0)
            return false;

        if (!items.ContainsKey(item))
            items.Add(item, 0);

        items[item] += amount;
        InvenUI.UpdateItem(item, items[item]);
        return true;
           
    }
}
