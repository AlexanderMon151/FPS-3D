using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform SlotsParent;
    public InventorySlotUI SlotPreFab;
    public readonly Dictionary<ItemData, InventorySlotUI> Slots = new();
    public void UpdateItem(ItemData item, int amount)
    {
        if (!Slots.ContainsKey(item))
        {
            InventorySlotUI slot = Instantiate(SlotPreFab, SlotsParent);
            Slots.Add(item, slot);
        }

        Slots[item].SetItem(item, amount);

    }
}