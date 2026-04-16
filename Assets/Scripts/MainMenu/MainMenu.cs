/*****************************************************************************
* File Name      : MainMenu.cs
* Author         : Noah Hurney
* Creation Date  : February 18, 2026
* Last Updated   : April 16, 2026
* Brief Description : Handles main menu navigation, including loading the
*                     map selection menu.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the map selection menu.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Loads the tutorial scene.
    /// </summary>
    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }

}
