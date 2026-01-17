using UnityEngine;

public class SpaceFighterController : MonoBehaviour
{
    [Header("Speeds")]
    public float forwardSpeed = 50f;
    public float boostMultiplier = 2f;

    [Header("Maneuverability")]
    public float lookSpeed = 90f;
    public float rollSpeed = 90f;

    // Private variables
    private Rigidbody rb;
    private float activeForwardSpeed;
    private float targetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // HIDES the mouse cursor so it doesn't fly off screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // 'W' and 'S' control forward/backward thrust
        // Mathf.Lerp makes the throttle feel like a heavy engine revving up
        float throttleInput = Input.GetAxis("Vertical");

        // Boost with Shift (optional)
        float currentMaxSpeed = Input.GetKey(KeyCode.LeftShift) ? forwardSpeed * boostMultiplier : forwardSpeed;

        targetSpeed = throttleInput * currentMaxSpeed;
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, targetSpeed, 2f * Time.fixedDeltaTime);

        // Apply force in the direction the ship is FACING
        rb.AddRelativeForce(Vector3.forward * activeForwardSpeed, ForceMode.Acceleration);
    }

    void HandleRotation()
    {
        // Mouse X (Left/Right) = Yaw
        float yaw = Input.GetAxis("Mouse X") * lookSpeed * Time.fixedDeltaTime;

        // Mouse Y (Up/Down) = Pitch
        float pitch = -Input.GetAxis("Mouse Y") * lookSpeed * Time.fixedDeltaTime; // Negative to pull up

        // A / D (Left/Right) = Roll
        float roll = -Input.GetAxis("Horizontal") * rollSpeed * Time.fixedDeltaTime;

        // Apply rotation to the physics body
        rb.AddRelativeTorque(new Vector3(pitch, yaw, roll), ForceMode.Force);
    }
}