/*****************************************************************************
* File Name      : ShopUpgrade.cs
* Author         : Noah Hurney
* Creation Date  : March 24, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles purchasing weapon upgrades from the shop,
*                     applying stat changes and hiding UI elements after use.
*****************************************************************************/

using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    public GameObject upgradeButton;
    public GameObject upgradeText;
    public int cost = 250;
    public float damageBoost = 0.2f;

    /// <summary>
    /// Attempts to purchase the damage upgrade and apply it to the player's gun.
    /// </summary>
    public void BuyDamageUpgrade()
    {
        if (ScoreManager.instance.score >= cost)
        {
            ScoreManager.instance.score -= cost;
            ScoreManager.instance.UpdateUI();

            GunStats gun = FindFirstObjectByType<GunStats>();
            gun.AddDamage(damageBoost);

            if (upgradeButton != null)
                upgradeButton.SetActive(false);

            if (upgradeText != null)
                upgradeText.SetActive(false);

            Debug.Log("Damage upgraded! New damage: " + gun.CurrentDamage);
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }
}