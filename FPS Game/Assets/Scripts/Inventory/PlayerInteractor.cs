using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    public Inventory PlayerInvetory;
    public TextMeshProUGUI Promt;
    List<PotionPickup> nearby = new();
    PotionPickup Focus => nearby.Count > 0 ? nearby[nearby.Count - 1] : null;

    public void Register(PotionPickup potion)
    {
        if (!nearby.Contains(potion)) nearby.Add(potion);
    }

    public void Unregister(PotionPickup potion) => nearby.Remove(potion);
    private void Update()
    {
        UpdatePromt();
        TryPickup();
    }
    private void UpdatePromt()
    {
        nearby.RemoveAll(p => p == null || !p.gameObject.activeInHierarchy);
        string text = "";

        if (Focus != null)
        {
            text = !Inventory.CanAdd(Focus.Data)
                ? "Inventory full"
                : "Press E to pickup:" + Focus.Data.DisplayName;
        }

        if (Promt.text != text) Promt.text = text;
        Promt.enabled = text.Length > 0;
    }

    private void TryPickup()
    {
        if (Focus == null)
            return;

        if (Keyboard.current?.eKey.wasPressedThisFrame = true)
            return;

        Focus.TryPickup(Inventory);
        UpdatePromt();
    }
}
