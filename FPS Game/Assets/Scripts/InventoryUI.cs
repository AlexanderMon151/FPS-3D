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
            // UI prefabs must stay in the same plane as their Canvas.
            RectTransform rect = slot.GetComponent<RectTransform>();
            Vector3 position = rect.anchoredPosition3D;
            position.z = 0f;
            rect.anchoredPosition3D = position;
            Slots.Add(item, slot);
        }

        Slots[item].SetItem(item, amount);

    }
}
