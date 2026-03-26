/*****************************************************************************
* File Name      : PlayerMovement.cs
* Author         : Noah Hurney
* Creation Date  : February 22, 2026
* Last Updated   : March 26, 2026
* Brief Description : Handles player movement using Rigidbody physics and the
*                     Unity Input System, including interaction input checks.
*****************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody myRb;
    private InputAction movementAction;
    private InputAction lookAction;
    private InputAction interactAction;

    private Vector2 moveInput;

    /// <summary>
    /// Initializes the Rigidbody and prevents unwanted rotation.
    /// </summary>
    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.freezeRotation = true;
    }

    /// <summary>
    /// Enables movement, look, and interaction input actions.
    /// </summary>
    private void OnEnable()
    {
        movementAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        interactAction = InputSystem.actions.FindAction("Interact");

        movementAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
    }

    /// <summary>
    /// Disables all input actions when the object is disabled.
    /// </summary>
    private void OnDisable()
    {
        movementAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
    }

    /// <summary>
    /// Reads movement input each frame.
    /// </summary>
    private void Update()
    {
        moveInput = movementAction.ReadValue<Vector2>();
    }

    /// <summary>
    /// Applies movement using Rigidbody physics.
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 worldMove = transform.TransformDirection(move) * moveSpeed;

        worldMove.y = myRb.linearVelocity.y;

        myRb.linearVelocity = worldMove;
    }

    /// <summary>
    /// Returns true if the interact button was pressed this frame.
    /// </summary>
    public bool InteractPressed()
    {
        return interactAction.WasPressedThisFrame();
    }
}