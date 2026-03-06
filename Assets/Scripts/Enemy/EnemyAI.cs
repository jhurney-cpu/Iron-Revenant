using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        // Look at player
        transform.LookAt(player);

        // Move forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionStay(Collision collision)
    {
        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(10f * Time.deltaTime); // damage per second
        }
    }
}