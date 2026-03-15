using UnityEngine;

public class BuyableDoor : MonoBehaviour
{
    public int cost = 500;
    public GameObject doorObject;

    private bool playerInRange = false;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();
            Debug.Log("Press F to buy door for " + cost);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerMovement = null;
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
            ScoreManager.instance.score -= cost;
            ScoreManager.instance.AddPoints(0); // refresh UI

            doorObject.SetActive(false);

            Debug.Log("Door purchased!");
        }
        else
        {
            Debug.Log("Not enough points!");
        }
    }
}