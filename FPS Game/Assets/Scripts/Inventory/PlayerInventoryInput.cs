using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryInput : MonoBehaviour
{

    public Inventory inventory;
    public PotionEffects effects;

    public void SelectItem()
    {
        if (Keyboard.current == null)
            return;

        var k = Keyboard.current;
        if (k.digit1Key.wasPressedThisFrame)
            inventory.Select(0);
        if (k.digit2Key.wasPressedThisFrame)
            inventory.Select(1);
        if (k.digit3Key.wasPressedThisFrame)
            inventory.Select(2);
        if (k.digit4Key.wasPressedThisFrame)
            inventory.Select(3);
        if (k.digit5Key.wasPressedThisFrame)
            inventory.Select(4);
        if (k.digit6Key.wasPressedThisFrame)
            inventory.Select(5);

        float scroll = Mouse.current != null ? Mouse.current.scroll.ReadValue().y : 0f;

        if (scroll > 0f)
            inventory.SelectNext(1);
        else if (scroll < 0f)
            inventory.SelectNext(-1);
    }

    public void UseItem()
    {
        if (Mouse.current?.rightButton.wasPressedThisFrame != true)
            return;

        ItemData item = inventory.Active;

        if (item == null) 
            return;
        if (effects.TryUse(item))
            inventory.ConsumeSelected();
    }
}
