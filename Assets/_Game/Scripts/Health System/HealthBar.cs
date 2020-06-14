using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private IntVariable maxHealth = null;
    [SerializeField] private IntVariable health = null;
    [SerializeField] private Image fill = null;
    [SerializeField] private Gradient healthColorGradient = null;

    private float healthPercent;

    private void Start()
    {
        healthPercent = (float)health.value / (float)maxHealth.value;
        fill.fillAmount = healthPercent;
        fill.color = healthColorGradient.Evaluate(fill.fillAmount);
    }

    private void Update()
    {
        healthPercent = (float)health.value / (float)maxHealth.value;
        if (fill.fillAmount != healthPercent)
        {
            fill.fillAmount = healthPercent;
            fill.color = healthColorGradient.Evaluate(fill.fillAmount);
        }
    }
}
