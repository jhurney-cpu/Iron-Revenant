using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;

    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;
    public float waypointReachDistance = 0.2f;

    private Transform player;
    private bool chasingPlayer = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("EnemyAI has NO WAYPOINTS assigned! Check your spawner.");
        }
    }

    private void Update()
    {
        if (!chasingPlayer)
            FollowTrack();
        else
            ChasePlayer();
    }

    private void FollowTrack()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        if (currentWaypoint >= waypoints.Length)
        {
            chasingPlayer = true;
            return;
        }

        Transform target = waypoints[currentWaypoint];

        Vector3 direction = (target.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < waypointReachDistance)
        {
            currentWaypoint++;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}