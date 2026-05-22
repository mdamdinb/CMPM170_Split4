using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private float horizontalSensitivity = 0.1f;
    [SerializeField] private float verticalSensitivity = 0.1f;

    [Header("Vertical Pitch Limits")]
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 15f;

    [Header("Horizontal Yaw Limits")]
    [SerializeField] private bool limitHorizontalRange = true;
    [SerializeField] private float maxYawAngle = 60f;

    private float currentPitch = 0f;
    private float currentYaw = 0f;
    private Mouse mouse;

    void Start()
    {
        mouse = Mouse.current;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (mouse == null) return;
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector2 mouseDelta = mouse.delta.ReadValue();

        // --- HORIZONTAL ---
        currentYaw -= mouseDelta.x * horizontalSensitivity;
        if (limitHorizontalRange)
            currentYaw = Mathf.Clamp(currentYaw, -maxYawAngle, maxYawAngle);

        // --- VERTICAL (inverted) ---
        currentPitch += mouseDelta.y * verticalSensitivity;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        // Euler always needs all three: X (pitch), Y (yaw), Z (roll — always 0)
        transform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    public Vector3 GetBarrelDirection()
    {
        return transform.forward;
    }

    public void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}