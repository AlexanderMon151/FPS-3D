using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;
    public bool Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (Collected)
            return;

        Inventory inv = other.GetComponent<Inventory>();

        if (inv == null)
            return;

        Collected = true;

        if(!inv.AddItem(item, amount))
        {
            Collected = false;
            return;
        }

        Destroy(gameObject);
    }
}
