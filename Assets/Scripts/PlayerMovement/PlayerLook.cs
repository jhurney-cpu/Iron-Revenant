/*****************************************************************************
* File Name      : PlayerLook.cs
* Author         : Noah Hurney
* Creation Date  : March 5, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles first-person camera rotation, including vertical
*                     look, horizontal body rotation, and recoil effects.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform playerBody;

    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    private Vector2 lookInput;
    private float xRotation = 0f;

    private float recoilOffset = 0f;
    private float recoilRecoverSpeed = 10f;

    private InputAction lookAction;

    /// <summary>
    /// Locks and hides the cursor when gameplay begins.
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Enables the look input action.
    /// </summary>
    private void OnEnable()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        lookAction?.Enable();
    }

    /// <summary>
    /// Disables the look input action.
    /// </summary>
    private void OnDisable()
    {
        lookAction?.Disable();
    }

    /// <summary>
    /// Reads raw look input each frame.
    /// </summary>
    private void Update()
    {
        lookInput = lookAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// Applies camera and body rotation after all Update calls.
    /// </summary>
    private void LateUpdate()
    {
        HandleLook();
    }

    /// <summary>
    /// Processes vertical camera rotation, horizontal body rotation,
    /// and recoil recovery.
    /// </summary>
    private void HandleLook()
    {
        float mouseX = lookInput.x * sensitivityX;
        float mouseY = lookInput.y * sensitivityY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        recoilOffset = Mathf.Lerp(recoilOffset, 0f, Time.deltaTime * recoilRecoverSpeed);

        cameraTransform.localRotation = Quaternion.Euler(xRotation + recoilOffset, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }

    /// <summary>
    /// Adds recoil to the camera and sets the recovery speed.
    /// </summary>
    public void AddRecoil(float amount, float recovery)
    {
        recoilOffset += amount;
        recoilRecoverSpeed = recovery;
    }
}