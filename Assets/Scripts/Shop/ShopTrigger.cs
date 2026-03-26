using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject shopMenu;

    private bool playerInRange = false;
    private PlayerMovement playerMovement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerMovement = other.GetComponent<PlayerMovement>();

            InteractUI.instance.Show("Press F to Open Shop");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerMovement = null;

            InteractUI.instance.Hide();
            CloseShop();
        }
    }

    private void Update()
    {
        if (playerInRange && playerMovement != null && playerMovement.InteractPressed())
        {
            OpenShop();
        }
    }

    private void OpenShop()
    {
        shopMenu.SetActive(true);
        InteractUI.instance.Hide();

        // Freeze player movement/look
        playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}