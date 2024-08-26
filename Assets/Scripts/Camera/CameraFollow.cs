using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform groundLevel;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] float yOffset = 10f;

    bool isGrounded;

    void Update()
    {
        if (playerTransform != null)
        {
            PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                isGrounded = playerMovement.isGrounded;
            }
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            float targetX = playerTransform.position.x;
            float targetY;

            bool isAtCameraBottom = playerTransform.position.y <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0.1f, Camera.main.nearClipPlane)).y;

            if (isGrounded || isAtCameraBottom)
            {
                targetY = playerTransform.position.y;
            }
            else
            {
                targetY = transform.position.y;
            }

            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,
                new Vector3(targetX, targetY, transform.position.z),
                smoothSpeed
            );
            transform.position = smoothedPosition;
        }
    }
}
