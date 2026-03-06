using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Sensitivity")]
    public float normalSensitivity = 100f;
    public float adsSensitivity = 50f;

    [Header("Camera FOV")]
    public Camera cam;
    public float normalFOV = 70f;
    public float adsFOV = 50f;
    public float fovSpeed = 10f;

    [Header("References")]
    public Transform playerBody;

    private InputAction lookAction;
    private InputAction aimAction;

    private float xRotation = 0f;
    private bool isAiming = false;

    private void OnEnable()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        aimAction = InputSystem.actions.FindAction("Aim");

        lookAction?.Enable();
        aimAction?.Enable();
    }

    private void OnDisable()
    {
        lookAction?.Disable();
        aimAction?.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (cam == null)
            cam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Check if aiming
        isAiming = aimAction.IsPressed();

        // Choose sensitivity
        float currentSensitivity = isAiming ? adsSensitivity : normalSensitivity;

        // Read mouse input
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float mouseX = lookInput.x * currentSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * currentSensitivity * Time.deltaTime;

        // Vertical rotation (camera only)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Horizontal rotation (player body)
        playerBody.Rotate(Vector3.up * mouseX);

        // Smooth FOV zoom
        float targetFOV = isAiming ? adsFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);
    }
}