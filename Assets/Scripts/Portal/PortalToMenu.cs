/*****************************************************************************
* File Name      : PortalToMenu.cs
* Author         : Noah Hurney
* Creation Date  : March 25, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles returning the player to the Main Menu scene
*                     when they enter the portal trigger.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToMenu : MonoBehaviour
{
    /// <summary>
    /// When the player enters the trigger, load the Main Menu scene.
    /// </summary>
    /// <param name="other">Collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}