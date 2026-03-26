/*****************************************************************************
* File Name      : GunShoot.cs
* Author         : Noah Hurney
* Creation Date  : March 2, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles projectile-based shooting, bullet spawning,
*                     muzzle flash effects, and input detection.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 50f;

    public GameObject muzzleFlashPrefab;

    private InputAction shootAction;

    /// <summary>
    /// Enables the shooting input action.
    /// </summary>
    private void OnEnable()
    {
        shootAction = InputSystem.actions.FindAction("Fire");
        shootAction?.Enable();
    }

    /// <summary>
    /// Disables the shooting input action.
    /// </summary>
    private void OnDisable()
    {
        shootAction?.Disable();
    }

    /// <summary>
    /// Checks for shooting input each frame.
    /// </summary>
    private void Update()
    {
        if (shootAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }

    /// <summary>
    /// Spawns a bullet and muzzle flash at the fire point.
    /// </summary>
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f);

        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.1f);
        }
    }
}