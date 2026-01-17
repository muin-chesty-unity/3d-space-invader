using UnityEngine;

public class CinematicCameraV2 : MonoBehaviour
{
    public Transform target;

    [Header("Settings")]
    public Vector3 offset = new Vector3(0, 2, -8);
    public float positionSmoothTime = 0.2f; // Lag for "Weight" (Higher = heavier)
    public float rotationSmoothTime = 0.05f; // Fast for "Aiming" (Lower = snappy)

    private Vector3 currentVelocity;
    private Quaternion currentRotationVel;

    void FixedUpdate()
    {
        if (target == null) return;

        // 1. Handle Position (Smooth and Heavy)
        // We calculate the target position relative to the ship's rotation
        Vector3 targetPos = target.position + (target.rotation * offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, positionSmoothTime);

        // 2. Handle Rotation (Snappy and Responsive)
        // We want the camera to look exactly where the ship is pointing, quickly
        Quaternion targetRot = target.rotation;
        // Use Slerp (Spherical Linear Interpolation) for smooth rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 1f / rotationSmoothTime * Time.fixedDeltaTime);
    }
}