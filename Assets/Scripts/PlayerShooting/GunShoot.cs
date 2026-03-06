using UnityEngine;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 2f;

    private InputAction shootAction;

    private void OnEnable()
    {
        shootAction = InputSystem.actions.FindAction("Fire");

        if (shootAction != null)
            shootAction.Enable();
        else
            Debug.LogError("Fire action not found! Make sure it's named 'Fire'.");
    }

    private void OnDisable()
    {
        if (shootAction != null)
            shootAction.Disable();
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.Rotate(90f, 0f, 0f); // adjust this until it looks right

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, 3f); // auto-cleanup
    }
}