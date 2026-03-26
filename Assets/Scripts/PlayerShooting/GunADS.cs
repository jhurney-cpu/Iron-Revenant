/*****************************************************************************
* File Name      : GunADS.cs
* Author         : Noah Hurney
* Creation Date  : March 5, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles aiming down sights by smoothly transitioning the
*                     weapon between hip and ADS positions, and toggling the
*                     reticle.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class GunADS : MonoBehaviour
{
    public Transform hipPosition;
    public Transform adsPosition;
    public float aimSpeed = 10f;

    public GameObject reticle;

    private InputAction aimAction;
    private bool isAiming = false;

    /// <summary>
    /// Enables the aim input action.
    /// </summary>
    private void OnEnable()
    {
        aimAction = InputSystem.actions.FindAction("Aim");
        aimAction?.Enable();
    }

    /// <summary>
    /// Disables the aim input action.
    /// </summary>
    private void OnDisable()
    {
        aimAction?.Disable();
    }

    /// <summary>
    /// Handles ADS logic and reticle visibility each frame.
    /// </summary>
    private void Update()
    {
        isAiming = aimAction.IsPressed();

        Transform target = isAiming ? adsPosition : hipPosition;

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

        if (reticle != null)
            reticle.SetActive(!isAiming);
    }
}