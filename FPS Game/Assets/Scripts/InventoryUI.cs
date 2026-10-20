using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public Transform SlotsParent;
    public InventorySlotUI SlotPreFab;
    // Match each item type to its existing UI slot so repeated pickups reuse it.
    public readonly Dictionary<ItemData, InventorySlotUI> Slots = new();
    public void Refresh(Inventory inventory)
    {
        // Start with all displayed items, then keep the ones still in the inventory.
        // Copy the keys so we can safely remove dictionary entries later.
        var removed = new List<ItemData>(Slots.Keys);
        for (int i = 0; i < inventory.Count; i++)
        {
            ItemData item = inventory.Get(i);
            UpdateItem(item, inventory.GetAmount(i));
            // Inventory indices start at 0, but displayed slot numbers start at 1.
            Slots[item].render(item, inventory.GetAmount(i), i == inventory.Selected, i + 1);
            removed.Remove(item);
        }
        // Delete UI slots for stacks that have been fully consumed or removed.
        foreach (ItemData item in removed)
        {
            Destroy(Slots[item].gameObject);
            Slots.Remove(item);
        }
    }

    public void UpdateItem(ItemData item, int amount)
    {
        if (!Slots.ContainsKey(item))
        {
            // Create the slot inside the inventory panel so its layout group positions it.
            InventorySlotUI slot = Instantiate(SlotPreFab, SlotsParent);
            // UI prefabs must stay in the same plane as their Canvas.
            RectTransform rect = slot.GetComponent<RectTransform>();
            Vector3 position = rect.anchoredPosition3D;
            position.z = 0f;
            rect.anchoredPosition3D = position;
            Slots.Add(item, slot);
        }

        Slots[item].render(item, amount, false, Slots.Count);

    }
}
