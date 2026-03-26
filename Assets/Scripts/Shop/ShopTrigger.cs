/*****************************************************************************
* File Name      : ShopTrigger.cs
* Author         : Noah Hurney
* Creation Date  : March 24, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles player interaction with the shop trigger, opening
*                     and closing the shop menu and managing player control.
*****************************************************************************/

using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopMenu;

    private bool playerInRange = false;
    private PlayerMovement playerMovement;

    /// <summary>
    /// Detects when the player enters the shop trigger and shows the prompt.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();

            InteractUI.instance.Show("Press F to Open Shop");
        }
    }

    /// <summary>
    /// Detects when the player exits the shop trigger and closes the shop.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerMovement = null;

            InteractUI.instance.Hide();
            CloseShop();
        }
    }

    /// <summary>
    /// Checks for interaction input while the player is inside the trigger.
    /// </summary>
    private void Update()
    {
        if (playerInRange && playerMovement != null && playerMovement.InteractPressed())
        {
            OpenShop();
        }
    }

    /// <summary>
    /// Opens the shop menu and disables player movement and look.
    /// </summary>
    private void OpenShop()
    {
        shopMenu.SetActive(true);
        InteractUI.instance.Hide();

        playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Closes the shop menu and restores player control.
    /// </summary>
    public void CloseShop()
    {
        shopMenu.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}