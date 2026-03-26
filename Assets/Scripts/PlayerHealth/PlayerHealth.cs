/*****************************************************************************
* File Name      : PlayerHealth.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles the player's health, taking damage, and triggering
*                     death behavior when health reaches zero.
*****************************************************************************/

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    /// <summary>
    /// Initializes the player's health.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies damage to the player and checks for death.
    /// </summary>
    /// <param name="amount">Amount of damage dealt.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles player death behavior.
    /// </summary>
    private void Die()
    {
        Debug.Log("Player died");
        // Later: restart level, show death screen, etc.
    }
}