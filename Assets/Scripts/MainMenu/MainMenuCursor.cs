/*****************************************************************************
* File Name      : MainMenuCursor.cs
* Author         : Noah Hurney
* Creation Date  : March 26, 2026
* Last Updated   : March 26, 2026
* Brief Description : Ensures the cursor is unlocked and visible when entering
*                     the main menu scene.
*****************************************************************************/

using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    /// <summary>
    /// Unlocks and shows the cursor when the main menu loads.
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}