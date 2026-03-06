using UnityEngine;
using UnityEngine.InputSystem;

public class GunADS : MonoBehaviour
{
    [Header("ADS Positions")]
    public Transform hipPosition;
    public Transform adsPosition;
    public float aimSpeed = 10f;

    [Header("UI")]
    public GameObject reticle;   // drag your reticle/dot here

    private InputAction aimAction;
    private bool isAiming = false;

    private void OnEnable()
    {
        aimAction = InputSystem.actions.FindAction("Aim");
        aimAction?.Enable();
    }

    private void OnDisable()
    {
        aimAction?.Disable();
    }

    private void Update()
    {
        // Check if right-click is held
        isAiming = aimAction.IsPressed();

        // Pick target position
        Transform target = isAiming ? adsPosition : hipPosition;

        // Smoothly move gun in LOCAL space
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            target.localPosition,
            Time.deltaTime * aimSpeed
        );

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            target.localRotation,
            Time.deltaTime * aimSpeed
        );

        // Toggle reticle visibility
        if (reticle != null)
            reticle.SetActive(!isAiming);
    }
}