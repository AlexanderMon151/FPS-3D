using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI SelectedIcon;
    public TextMeshProUGUI label;
    public TextMeshProUGUI AmountText;

    public void render(ItemData item, int amount, bool selected, int number)
    {
        // Hide the image when no item icon is available.
        Icon.sprite = item != null ? item.Icon : null;
        Icon.enabled = Icon.sprite != null;
        string name = item != null ? item.displayName + " x" + amount : "Empty";
        // These text fields are optional: a prefab can show a label, a count, or both.
        if (label != null)
            label.text = number + ". " + name;
        if (AmountText != null)
            AmountText.text = amount.ToString();
        if (SelectedIcon != null)
        {
            // Only show the selection marker on the currently selected slot.
            SelectedIcon.gameObject.SetActive(selected);
            SelectedIcon.enabled = selected;
        }
    }

}
