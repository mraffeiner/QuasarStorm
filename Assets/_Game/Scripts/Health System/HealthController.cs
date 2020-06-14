using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public static event Action PlayerDied;
    public static event Action<GameObject> DeathEvent;

    [SerializeField] private IntVariable maxHealth;
    [SerializeField] private IntVariable health;
    [SerializeField] private int damageVelocityThreshold = 1;

    private void Awake()
    {
        if (maxHealth == null)
        {
            maxHealth = ScriptableObject.CreateInstance<IntVariable>();
            maxHealth.value *= 1 + (int)transform.localScale.magnitude;
        }
        if (health == null)
        {
            health = ScriptableObject.CreateInstance<IntVariable>();
            health.value *= 1 + (int)transform.localScale.magnitude;
        }
    }

    private void OnEnable() => health.value = maxHealth.value;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > damageVelocityThreshold)
            TakeDamage((int)(collision.relativeVelocity.magnitude - damageVelocityThreshold));
    }

    public void TakeDamage(int value)
    {
        health.value -= value;

        if (health.value <= 0)
            InitiateDeath();
    }

    // Deactivate instead of destroying to be able to utilize object pooling
    private void InitiateDeath()
    {
        if (tag == "Player")
            PlayerDied?.Invoke();
        else
            DeathEvent?.Invoke(this.gameObject);

        this.gameObject.SetActive(false);
    }
}
