using UnityEngine;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 50f;

    [Header("Muzzle Flash")]
    public GameObject muzzleFlashPrefab;   // drag your muzzle flash prefab here

    private InputAction shootAction;

    private void OnEnable()
    {
        shootAction = InputSystem.actions.FindAction("Fire");
        shootAction?.Enable();
    }

    private void OnDisable()
    {
        shootAction?.Disable();
    }

    private void Update()
    {
        if (shootAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Spawn bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f);

        // Spawn muzzle flash
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.1f); // muzzle flash is very quick
        }
    }
}