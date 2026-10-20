using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryUI InvenUI;
    // Capacity limits different item types; repeated pickups share the same stack.
    public const int Capacity = 6;
    readonly List<ItemStack> Items = new();
    // -1 means that no item is selected, for example when the inventory is empty.
    int selected = -1;
    public int Count => Items.Count;
    public int Selected => selected;
    public ItemData Active => selected >= 0 && selected < Items.Count ? Items[selected].item : null;
    public ItemData Get(int index) => Items[index].item;
    public int GetAmount(int index) => Items[index].amount;
    public event Action Changed;

    
    private void OnEnable() => Changed += RefreshUI;
    private void OnDisable() => Changed -= RefreshUI;
    
    private void Start() => RefreshUI();

    private void RefreshUI()
    {
        if (InvenUI != null)
            InvenUI.Refresh(this);
    }

    public bool TryAdd(ItemData item, int amount = 1)
    {
        if (amount <= 0 || !CanAdd(item))
            return false;
        ItemStack stack = Items.Find(x => x.item == item);

        /
        if (stack != null)
            stack.amount += amount;
        else
        {
            Items.Add(new ItemStack(item, amount));
            if (selected < 0)
                selected = 0;
        }

        
        Changed?.Invoke();
        return true;
    }

    public bool CanAdd(ItemData item)
    {
        
        return item != null && (Items.Exists(x => x.item == item)
            || Count < Capacity);
    }

    public void Select(int index)
    {
        if (index < 0 || index >= Count || index == selected)
            return;

        selected = index;
        Changed?.Invoke();
    }

    public void SelectNext(int step)
    {
        if (Count == 0)
            return;

        
        Select((selected + step + Count) % Count);
    }

    public void ConsumeSelected(int step)
    {
        if (selected < 0)
            return;
        Items[selected].amount--;

        if (Items[selected].amount == 0)
        {
            Items.RemoveAt(selected);
            selected = Mathf.Min(selected, Count - 1);
        }
        Changed?.Invoke();
    }
    
}
