using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;
    public Transform playerBody;   // NEW: rotate this for horizontal look

    [Header("Sensitivity")]
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    private Vector2 lookInput;
    private float xRotation = 0f;

    // Recoil support
    private float recoilOffset = 0f;
    private float recoilRecoverSpeed = 10f;

    private InputAction lookAction;

    private void OnEnable()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        lookAction?.Enable();
    }

    private void OnDisable()
    {
        lookAction?.Disable();
    }

    private void Update()
    {
        // Read input ONLY
        lookInput = lookAction.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        HandleLook();
    }

    private void HandleLook()
    {
        float mouseX = lookInput.x * sensitivityX;
        float mouseY = lookInput.y * sensitivityY;

        // Vertical rotation (camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply recoil
        recoilOffset = Mathf.Lerp(recoilOffset, 0f, Time.deltaTime * recoilRecoverSpeed);

        cameraTransform.localRotation = Quaternion.Euler(xRotation + recoilOffset, 0f, 0f);

        // Horizontal rotation (player body)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void AddRecoil(float amount, float recovery)
    {
        recoilOffset += amount;
        recoilRecoverSpeed = recovery;
    }
}