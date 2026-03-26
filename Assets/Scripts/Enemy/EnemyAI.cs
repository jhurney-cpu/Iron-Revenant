/*****************************************************************************
* File Name      : EnemyAI.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : March 26, 2026
* Brief Description : Controls enemy movement along waypoint paths and transitions
*                     into chasing the player once the path is completed.
*****************************************************************************/

using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;
    public float waypointReachDistance = 0.2f;



    private int currentWaypoint = 0;
    private Transform player;
    private bool chasingPlayer = false;



    /// <summary>
    /// Initializes references and validates waypoint setup.
    /// </summary>
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogError("EnemyAI has NO WAYPOINTS assigned! Check your spawner.");
        }
    }

    /// <summary>
    /// Updates enemy behavior each frame, switching between pathing and chasing.
    /// </summary>
    private void Update()
    {
        if (!chasingPlayer)
            FollowTrack();
        else
            ChasePlayer();
    }


    /// <summary>
    /// Moves the enemy along the assigned waypoint path until completed.
    /// </summary>
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

    /// <summary>
    /// Moves the enemy toward the player's position once pathing is complete.
    /// </summary>
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