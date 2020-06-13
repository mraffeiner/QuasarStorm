using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health = 1;

    public void TakeDamage(int value)
    {
        health -= value;

        if (health <= 0)
            InitiateDeath();
    }

    private void InitiateDeath() => Destroy(this.gameObject);
}
