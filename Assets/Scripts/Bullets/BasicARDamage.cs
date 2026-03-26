/*****************************************************************************
* File Name      : BulletDamage.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles applying damage to enemies when a bullet makes
*                     contact, using the current damage value from the player's
*                     equipped weapon.
*****************************************************************************/

using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    /// <summary>
    /// The amount of damage this bullet will deal, pulled from the active gun.
    /// </summary>
    private float damage;


    /// <summary>
    /// Retrieves the current damage value from the player's equipped gun.
    /// </summary>
    private void Start()
    {
        GunStats gun = FindFirstObjectByType<GunStats>();
        damage = gun.CurrentDamage;
    }

    /// <summary>
    /// Trigger-based collision detection for bullets using trigger colliders.
    /// </summary>
    /// <param name="other">The collider the bullet has entered.</param>
    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    /// <summary>
    /// Physics-based collision detection for bullets using non-trigger colliders.
    /// </summary>
    /// <param name="collision">Collision information from Unity's physics system.</param>
    private void OnCollisionEnter(Collision collision)
    {
        TryDamage(collision.collider.gameObject);
    }


    /// <summary>
    /// Attempts to apply damage to any object containing a ZombieHealth component.
    /// </summary>
    /// <param name="hitObject">The object the bullet collided with.</param>
    private void TryDamage(GameObject hitObject)
    {
        ZombieHealth enemy = hitObject.GetComponent<ZombieHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}