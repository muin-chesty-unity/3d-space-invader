using UnityEngine;

public class CinematicPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 50f;
    public float turnSpeed = 100f;

    private Rigidbody rb;
    private float moveInput;
    private float turnInput;

    void Start()
    {
        // Get the Rigidbody we added earlier
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from keyboard (WASD or Arrow Keys)
        moveInput = Input.GetAxis("Vertical");   // W/S
        turnInput = Input.GetAxis("Horizontal"); // A/D
    }

    void FixedUpdate()
    {
        // Physics calculations happen here for "heavy" movement
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Apply a forward force based on the ship's orientation
        Vector3 movement = transform.forward * moveInput * moveSpeed;
        rb.AddForce(movement, ForceMode.Acceleration);
        Debug.Log($"Moving???");
    }

    void HandleRotation()
    {
        // Rotate the ship left and right
        float rotation = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}