using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth = 100;
    public int Max => Mathf.Max(1, MaxHealth);
    public int Current => Mathf.Clamp(CurrentHealth, 0, Max);
    public event Action<int, int> HealthChanged;

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        SetHealth(Current - amount);
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        SetHealth(Current + amount);
    }

    void SetHealth(int value)
    {
        int next = Mathf.Clamp(value, 0, Max);

        if (next == Current)
            return;

        CurrentHealth = next;
        HealthChanged?.Invoke(Current, Max);
    }
}