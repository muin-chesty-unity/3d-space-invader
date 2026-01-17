using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    public Transform target;        // Drag your PlayerShip here
    public Vector3 offset = new Vector3(0, 3, -10); // Position relative to ship
    public float smoothSpeed = 0.125f; // Higher = snappier, Lower = heavier

    private Vector3 currentVelocity = Vector3.zero;

    void LateUpdate() // LateUpdate runs after the ship has moved
    {
        if (target == null) return;

        // Calculate where the camera WANTS to be
        Vector3 desiredPosition = target.TransformPoint(offset);

        // Smoothly slide from current position to desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);

        // Always keep the ship in the center of the frame
        transform.LookAt(target);
    }
}