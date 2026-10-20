using UnityEngine;

public enum ItemKind { HealthPotion, SpeedPotion, HighJumpPotion }

[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite Icon;
    public ItemKind kind;
    public int healAmount = 25;
    public float multiplier = 1.5f;
    public float duration = 10f;

}
