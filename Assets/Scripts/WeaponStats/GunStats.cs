/*****************************************************************************
* File Name      : GunStats.cs
* Author         : Noah Hurney
* Creation Date  : March 8, 2026
* Last Updated   : March 26, 2026
* Brief Description : Stores and manages weapon damage values, including base
*                     damage and upgradeable multipliers.
*****************************************************************************/

using UnityEngine;

public class GunStats : MonoBehaviour
{
    public float baseDamage = 20f;
    public float damageMultiplier = 1f;

    /// <summary>
    /// The current calculated damage of the weapon.
    /// </summary>
    public float CurrentDamage => baseDamage * damageMultiplier;

    /// <summary>
    /// Increases the weapon's damage multiplier.
    /// </summary>
    public void AddDamage(float amount)
    {
        damageMultiplier += amount;
    }
}