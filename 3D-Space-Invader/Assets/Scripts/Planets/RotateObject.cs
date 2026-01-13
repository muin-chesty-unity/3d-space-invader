using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public bool shouldRotate = true;
    public Vector3 rotationSpeed = new Vector3(0, 5, 0); // Degrees per second

    void Update()
    {
        if (shouldRotate)
        {
            // Rotate the object around its own axis smoothly
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
