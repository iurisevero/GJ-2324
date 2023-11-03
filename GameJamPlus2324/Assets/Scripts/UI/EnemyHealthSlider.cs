using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider slider;
    [SerializeField] private Health health;

    private void Awake()
    {
        canvas.worldCamera = Camera.main;
    }

    void Start()
    {
        slider.maxValue = health.maxHealth;
        slider.value = health.currentHealth;
        health.OnHealthValueChanged += UpdateSlider;
    }

    void UpdateSlider(int currentHealth)
    {
        slider.value = health.currentHealth;
    }

    private void OnDestroy()
    {
        health.OnHealthValueChanged -= UpdateSlider;
    }

    private void OnEnable()
    {
        if(health is null) return;
        health.currentHealth = health.maxHealth;
        UpdateSlider(health.currentHealth);
    }
}