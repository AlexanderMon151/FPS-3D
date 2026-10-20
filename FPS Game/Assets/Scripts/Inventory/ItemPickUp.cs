using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData item;
    public int amount = 1;
    public bool Collected;

    private void OnTriggerEnter(Collider other)
    {
        // Prevent multiple colliders from collecting this object more than once.
        if (Collected)
            return;

        // A player's collider may be on a child object while Inventory is on its parent.
        Inventory inv = other.GetComponentInParent<Inventory>();

        if (inv == null)
            return;

        Collected = true;

        if(!inv.TryAdd(item, amount))
        {
            // Leave the pickup in the world if the inventory cannot accept it.
            Collected = false;
            return;
        }

        // Hide immediately, then remove the world object at the end of the frame.
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
