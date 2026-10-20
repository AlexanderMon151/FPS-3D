using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health PlayerHealth;
    public Slider SliderBar;
    public TextMeshProUGUI Label;

    void Render(int current, int max)
    {
        Label.text = $"HP: {current}/{max}";
        SliderBar.SetValueWithoutNotify((float)current / max);
    }

    void OnEnable()
    {
        PlayerHealth.HealthChanged += Render;
        Render(PlayerHealth.Current, PlayerHealth.Max);
    }

    void OnDisable()
    {
        if (PlayerHealth != null)
            PlayerHealth.HealthChanged -= Render;
    }
}