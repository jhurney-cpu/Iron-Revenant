/*****************************************************************************
* File Name      : ZombieHealth.cs
* Author         : Noah Hurney
* Creation Date  : February 22, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles zombie health, taking damage, awarding points on
*                     death, and notifying the wave manager.
*****************************************************************************/

using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private float zombieMaxHealth = 100f;
    private float currentHealth;

    public int pointsOnDeath = 50;

    /// <summary>
    /// Initializes the zombie's health.
    /// </summary>
    private void Start()
    {
        currentHealth = zombieMaxHealth;
    }

    /// <summary>
    /// Applies damage to the zombie and checks for death.
    /// </summary>
    /// <param name="amount">Amount of damage dealt.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Awards points, notifies the wave manager, and destroys the zombie.
    /// </summary>
    private void Die()
    {
        ScoreManager.instance.AddPoints(pointsOnDeath);
        WaveManager.instance.ZombieDied();
        Destroy(gameObject);
    }
}