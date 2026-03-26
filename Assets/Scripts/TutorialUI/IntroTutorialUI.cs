/*****************************************************************************
* File Name      : IntroTutorialUI.cs
* Author         : Noah Hurney
* Creation Date  : March 26, 2026
* Last Updated   : March 26, 2026
* Brief Description : Displays a full-screen tutorial panel when the game
*                     starts, pausing gameplay and hiding the reticle.
*****************************************************************************/

using UnityEngine;

public class IntroTutorialUI : MonoBehaviour
{
    public GameObject tutorialPanel;
    public float displayTime = 10f;

    public GameObject reticle;

    /// <summary>
    /// Shows the tutorial panel, hides the reticle, and pauses the game.
    /// </summary>
    private void Start()
    {
        tutorialPanel.SetActive(true);

        if (reticle != null)
            reticle.SetActive(false);

        Time.timeScale = 0f;

        StartCoroutine(HideAfterDelay());
    }

    /// <summary>
    /// Waits in real time, hides the panel, restores the reticle, and resumes gameplay.
    /// </summary>
    private System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSecondsRealtime(displayTime);

        tutorialPanel.SetActive(false);

        if (reticle != null)
            reticle.SetActive(true);

        Time.timeScale = 1f;
    }
}