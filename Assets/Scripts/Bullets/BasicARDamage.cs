using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] float damage = 25f;

    private void OnCollisionEnter(Collision collision)
    {
        ZombieHealth enemy = collision.collider.GetComponent<ZombieHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}