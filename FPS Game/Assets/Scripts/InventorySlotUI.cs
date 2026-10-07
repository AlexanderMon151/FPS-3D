using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI AmountText;

    public void SetItem(ItemData item, int amount)
    {
        Icon.sprite = item.Icon;
        Icon.enabled = true;
        AmountText.text = amount.ToString();
    }

}
