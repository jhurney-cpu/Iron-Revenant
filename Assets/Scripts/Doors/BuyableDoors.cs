using UnityEngine;

public class BuyableDoor : MonoBehaviour
{
    public int cost = 500;
    public GameObject doorObject;

    [Header("Zone To Activate")]
    public SpawnerZone zoneToActivate;

    private bool playerInRange = false;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();

            InteractUI.instance.Show($"Press F to Buy Door [{cost}]");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerMovement = null;

            InteractUI.instance.Hide();
        }
    }

    private void Update()
    {
        if (playerInRange && playerMovement != null && playerMovement.InteractPressed())
        {
            TryBuyDoor();
        }
    }

    private void TryBuyDoor()
    {
        if (ScoreManager.instance.score >= cost)
        {
            // Deduct points
            ScoreManager.instance.score -= cost;
            ScoreManager.instance.AddPoints(0);

            // Disable the door mesh
            if (doorObject != null)
                doorObject.SetActive(false);

            // Activate the next zone
            if (zoneToActivate != null)
                zoneToActivate.isActive = true;

            // Force-hide UI
            playerInRange = false;
            InteractUI.instance.Hide();

            // Disable the trigger object itself
            gameObject.SetActive(false);
        }
        else
        {
            InteractUI.instance.Show("Not enough points!");
        }
    }
}