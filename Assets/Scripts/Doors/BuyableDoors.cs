/*****************************************************************************
* File Name      : BuyableDoor.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles player interaction with buyable doors, deducting
*                     points, disabling the door, activating new zones, and
*                     managing UI prompts.
*****************************************************************************/

using UnityEngine;

public class BuyableDoor : MonoBehaviour
{


    public int cost = 500;
    public GameObject doorObject;
    public SpawnerZone zoneToActivate;



    /// <summary>
    /// Tracks whether the player is within the interaction trigger.
    /// </summary>
    private bool playerInRange = false;

    /// <summary>
    /// Reference to the player's movement script for input detection.
    /// </summary>
    private PlayerMovement playerMovement;



    /// <summary>
    /// Detects when the player enters the door's trigger zone and displays the
    /// interaction UI prompt.
    /// </summary>
    /// <param name="other">Collider entering the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();

            InteractUI.instance.Show($"Press F to Buy Door [{cost}]");
        }
    }

    /// <summary>
    /// Detects when the player leaves the door's trigger zone and hides the
    /// interaction UI prompt.
    /// </summary>
    /// <param name="other">Collider exiting the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerMovement = null;

            InteractUI.instance.Hide();
        }
    }

    /// <summary>
    /// Checks for player input while in range to attempt purchasing the door.
    /// </summary>
    private void Update()
    {
        if (playerInRange && playerMovement != null && playerMovement.InteractPressed())
        {
            TryBuyDoor();
        }
    }



    /// <summary>
    /// Attempts to purchase the door. Deducts points, disables the door,
    /// activates the next zone, hides UI, and disables the trigger.
    /// </summary>
    private void TryBuyDoor()
    {
        if (ScoreManager.instance.score >= cost)
        {
            // Deduct points
            ScoreManager.instance.score -= cost;
            ScoreManager.instance.AddPoints(0); // Forces UI refresh

            // Disable the door mesh
            if (doorObject != null)
                doorObject.SetActive(false);

            // Activate the next zone
            if (zoneToActivate != null)
                zoneToActivate.isActive = true;

            // Hide UI and disable trigger
            playerInRange = false;
            InteractUI.instance.Hide();
            gameObject.SetActive(false);
        }
        else
        {
            InteractUI.instance.Show("Not enough points!");
        }
    }
}