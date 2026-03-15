using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody myRb;
    private InputAction movementAction;
    private InputAction lookAction;
    private InputAction interactAction;

    private Vector2 moveInput;

    private void Awake()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.freezeRotation = true;
    }

    private void OnEnable()
    {
        movementAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        interactAction = InputSystem.actions.FindAction("Interact");

        movementAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
    }

    private void Update()
    {
        moveInput = movementAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 worldMove = transform.TransformDirection(move) * moveSpeed;

        worldMove.y = myRb.linearVelocity.y;

        myRb.linearVelocity = worldMove;
    }

    public bool InteractPressed()
    {
        return interactAction.WasPressedThisFrame();
    }
}