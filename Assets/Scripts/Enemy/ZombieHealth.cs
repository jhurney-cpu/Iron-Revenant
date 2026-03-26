using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float zombieMaxHealth = 100f;
    private float currentHealth;

    [Header("Points")]
    public int pointsOnDeath = 50;

    private void Start()
    {
        currentHealth = zombieMaxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        ScoreManager.instance.AddPoints(pointsOnDeath);
        WaveManager.instance.ZombieDied();
        Destroy(gameObject);
    }
}