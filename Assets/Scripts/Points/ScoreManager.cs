/*****************************************************************************
* File Name      : ScoreManager.cs
* Author         : Noah Hurney
* Creation Date  : March 7, 2026
* Last Updated   : March 26, 2026
* Brief Description : Tracks and updates the player's score, providing a global
*                     access point for adding points and refreshing the UI.
*****************************************************************************/

using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// Assigns the singleton instance.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Initializes the score UI on start.
    /// </summary>
    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Adds points to the player's score and refreshes the UI.
    /// </summary>
    public void AddPoints(int amount)
    {
        score += amount;
        UpdateUI();
    }

    /// <summary>
    /// Updates the score text on the HUD.
    /// </summary>
    public void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
}