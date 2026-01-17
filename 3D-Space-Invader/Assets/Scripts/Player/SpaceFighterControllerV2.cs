using UnityEngine;

public class SpaceFighterControllerV2 : MonoBehaviour
{
    [Header("Speeds")]
    public float forwardSpeed = 50f;
    public float boostMultiplier = 2f;

    [Header("Maneuverability")]
    public float lookSpeed = 90f;
    public float rollSpeed = 90f;
    public float autoStabilizeSpeed = 2.0f; // How fast it corrects itself

    private Rigidbody rb;
    private float activeForwardSpeed;
    private float targetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleStabilization(); // New function to stop the "Free Fall" feeling
    }

    void HandleMovement()
    {
        float throttleInput = Input.GetAxis("Vertical");
        float currentMaxSpeed = Input.GetKey(KeyCode.LeftShift) ? forwardSpeed * boostMultiplier : forwardSpeed;

        targetSpeed = throttleInput * currentMaxSpeed;
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, targetSpeed, 2f * Time.fixedDeltaTime);

        rb.AddRelativeForce(Vector3.forward * activeForwardSpeed, ForceMode.Acceleration);
    }

    void HandleRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * lookSpeed * Time.fixedDeltaTime;
        float pitch = -Input.GetAxis("Mouse Y") * lookSpeed * Time.fixedDeltaTime;
        float roll = -Input.GetAxis("Horizontal") * rollSpeed * Time.fixedDeltaTime;

        rb.AddRelativeTorque(new Vector3(pitch, yaw, roll), ForceMode.Force);
    }

    void HandleStabilization()
    {
        // If the player is NOT manually rolling (not pressing A or D)
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f)
        {
            // Calculate the rotation needed to get back to "flat" (Up is Up)
            Vector3 predictedUp = Quaternion.AngleAxis(rb.angularVelocity.magnitude * Mathf.Rad2Deg * Time.fixedDeltaTime, rb.angularVelocity) * transform.up;
            Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);

            // Apply a force to bring the ship upright
            rb.AddTorque(torqueVector * autoStabilizeSpeed * autoStabilizeSpeed, ForceMode.Acceleration);
        }
    }
}