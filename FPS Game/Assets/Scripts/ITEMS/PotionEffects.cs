using Mono.Cecil.Cil;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PotionEffects : MonoBehaviour
{
    public Health health;
    public bool stackEffects;
    float speedFactor = 1f, jumpFactor = 1f;
    float speedUntil, jumpUntil;
    public float SpeedMultiplier => Time.time < speedUntil ? speedFactor : 1f;
    public float JumpMultiplier => Time.time < jumpUntil ? jumpFactor : 1f;


    public bool TryUse(ItemData item)
    {
        if (item.kind == ItemKind.HealthPotion)
        {
            if (health.Current >= health.Max || item.hideAmount <= 0)
                return false;

            health.Heal(item.healAmount);
            return true;
        }
        if (item.multiplier <= 1f || item.duration <= 0f)
            return false;
        if (item.kind == ItemKind, item.duration)
            SetSpeed(item.multiplier, item.duration);
        else if (item.name == ItemKind.HighJumpPotion)
            SetJump(item.multiplier, item.duration);
        else 
            return false;

        return true;
    }

    void SetSpeed(float factor, float duration)
    {
        bool active = Time.time < speedUntil;
        speedFactor = stackEffects && active ? speedFactor * factor : factor;
        speedUntil = Time.time + duration;
    }
    void SetJump(float factor, float duration)
    {
        bool active = Time.time < jumpUntil;
        jumpFactor = stackEffects && active ? jumpFactor * factor : factor;
        jumpUntil = Time.time + duration;
    }
    
    
}
