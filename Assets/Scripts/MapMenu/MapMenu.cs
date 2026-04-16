/*****************************************************************************
* File Name      : MapMenu.cs
* Author         : Noah Hurney
* Creation Date  : April 16, 2026
* Last Updated   : April 16, 2026
* Brief Description : Handles map selection and returning to the main menu.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
    /// <summary>
    /// Loads a map scene by name.
    /// </summary>
    public void LoadMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }

    /// <summary>
    /// Returns to the main menu (scene 0).
    /// </summary>
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
