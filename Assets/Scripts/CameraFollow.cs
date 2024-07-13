using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the player
    public Vector2 minXAndMaxX = new Vector2(-5, 5);  // Min and max X positions for the camera

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate desired position
            Vector3 desiredPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

            // Apply limits on X-axis
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minXAndMaxX.x, minXAndMaxX.y);

            // Smoothly move towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}