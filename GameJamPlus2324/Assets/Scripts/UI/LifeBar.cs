using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Health health;

    void Start()
    {
        image.fillAmount = (float)health.currentHealth / health.maxHealth;
        health.OnHealthValueChanged += UpdateSlider;
    }

    void UpdateSlider(int currentHealth)
    {
        image.fillAmount = (float)currentHealth / health.maxHealth;
    }

    private void OnDestroy()
    {
        health.OnHealthValueChanged -= UpdateSlider;
    }
}