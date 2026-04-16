/*****************************************************************************
* File Name      : EnemyAI.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : April 16, 2026
* Brief Description : Controls enemy movement along waypoint paths, transitions
*                     into chasing the player, and handles climbing obstacles.
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
    private bool isClimbing = false;

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
        if (isClimbing)
            return;

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

        // Trigger climb when reaching waypoint 2
        if (currentWaypoint == 2)
        {
            StartCoroutine(ClimbWallRoutine());
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

    /// <summary>
    /// Smoothly climbs the wall, waits, then drops back down before continuing.
    /// </summary>
    private System.Collections.IEnumerator ClimbWallRoutine()
    {
        isClimbing = true;

        float climbHeight = 1.5f;
        float climbForward = 3f;
        float climbTime = 1.6f;
        float dropDelay = 0f;
        float dropTime = 0.6f;

        Vector3 start = transform.position;
        Vector3 top = start + new Vector3(0, climbHeight, 0);
        Vector3 forward = top + transform.forward * climbForward;

        float t = 0f;

        // Move upward
        while (t < 1f)
        {
            t += Time.deltaTime / climbTime;
            transform.position = Vector3.Lerp(start, top, t);
            yield return null;
        }

        t = 0f;
        start = transform.position;

        // Move forward over the wall
        while (t < 1f)
        {
            t += Time.deltaTime / climbTime;
            transform.position = Vector3.Lerp(start, forward, t);
            yield return null;
        }

        // Wait on top
        yield return new WaitForSeconds(dropDelay);

        // Drop straight down
        Vector3 dropStart = transform.position;
        Vector3 dropEnd = new Vector3(dropStart.x, dropStart.y - climbHeight, dropStart.z);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / dropTime;
            transform.position = Vector3.Lerp(dropStart, dropEnd, t);
            yield return null;
        }

        // Continue to next waypoint
        currentWaypoint++;
        isClimbing = false;
    }
}
