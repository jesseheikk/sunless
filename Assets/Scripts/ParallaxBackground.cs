using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeedX = 1.0f;  
    public float parallaxSpeedY = 1.0f;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        float scaleFactor = transform.localScale.x;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * scaleFactor;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(cameraDelta.x * parallaxSpeedX, cameraDelta.y * parallaxSpeedY);
        previousCameraPosition = cameraTransform.position;
    }
}
