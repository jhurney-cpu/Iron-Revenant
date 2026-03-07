using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int pointsOnDeath = 50;
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
        ScoreManager.instance.AddPoints(pointsOnDeath);
        Destroy(gameObject);

        // Later: play animation, spawn particles, drop loot, etc.
        Destroy(gameObject);
    }
}