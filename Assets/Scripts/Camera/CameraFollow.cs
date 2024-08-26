using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Transform groundLevel;
    public float smoothSpeed = 0.125f;

    private bool isGrounded;
    public float yOffset = 10f;

    private void Update()
    {
        // Get the player's grounded state from the PlayerMovement script
        if (playerTransform != null)
        {
            PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                isGrounded = playerMovement.isGrounded;
            }
        }
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the target position for the camera
            float targetX = playerTransform.position.x;
            float targetY;

            bool isAtCameraBottom = playerTransform.position.y <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0.1f, Camera.main.nearClipPlane)).y;

            // Adjust the Y position if the player is grounded or falling of the screen
            if (isGrounded || isAtCameraBottom)
            {
                targetY = playerTransform.position.y;
            }
            // Otherwise keep the Y position as is
            else
            {
                targetY = transform.position.y;
            }

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                new Vector3(targetX, targetY, transform.position.z),
                smoothSpeed
            );
            transform.position = smoothedPosition;
        }
    }
}
