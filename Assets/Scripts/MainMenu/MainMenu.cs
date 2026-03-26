/*****************************************************************************
* File Name      : MainMenu.cs
* Author         : Noah Hurney
* Creation Date  : February 18, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles loading the tutorial scene from the main menu.
*****************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the tutorial scene.
    /// </summary>
    public void LoadTutorial()
    {
        SceneManager.LoadScene(1);
    }
}