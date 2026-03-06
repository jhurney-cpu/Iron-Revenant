using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] float zombieMaxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = zombieMaxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Later: play animation, spawn particles, drop loot, etc.
        Destroy(gameObject);
    }
}