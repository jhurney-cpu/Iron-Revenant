using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Rigidbody myRb;
    private InputAction movementAction;
    private InputAction lookAction;
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
        movementAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        lookAction.Disable();
    }

    private void Update()
    {
        moveInput = movementAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Convert input to 3D movement
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 worldMove = transform.TransformDirection(move) * moveSpeed;

        // Keep gravity working
        worldMove.y = myRb.linearVelocity.y;

        // Apply velocity
        myRb.linearVelocity = worldMove;
    }
}