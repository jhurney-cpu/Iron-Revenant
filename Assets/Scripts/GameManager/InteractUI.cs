using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    public static InteractUI instance;

    public TextMeshProUGUI interactText;

    private void Awake()
    {
        instance = this;
        interactText.gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        interactText.text = message;
        interactText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        interactText.gameObject.SetActive(false);
    }
}