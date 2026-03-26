/*****************************************************************************
* File Name      : InteractUI.cs
* Author         : Noah Hurney
* Creation Date  : February 23, 2026
* Last Updated   : March 26, 2026
* Brief Description : Controls the interaction UI prompt, allowing scripts to
*                     show or hide messages when the player can interact.
*****************************************************************************/

using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    public static InteractUI instance;

    public TextMeshProUGUI interactText;

    /// <summary>
    /// Initializes the singleton instance and hides the interaction text.
    /// </summary>
    private void Awake()
    {
        instance = this;
        interactText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Displays an interaction message on screen.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void Show(string message)
    {
        interactText.text = message;
        interactText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the interaction message.
    /// </summary>
    public void Hide()
    {
        interactText.gameObject.SetActive(false);
    }
}