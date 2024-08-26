using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] float parallaxSpeedX = 1.0f;  
    [SerializeField] float parallaxSpeedY = 1.0f;

    Transform cameraTransform;
    Vector3 previousCameraPosition;
    float textureUnitSizeX;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        float scaleFactor = transform.localScale.x;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * scaleFactor;
    }

    void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;
        transform.position += new Vector3(cameraDelta.x * parallaxSpeedX, cameraDelta.y * parallaxSpeedY);
        previousCameraPosition = cameraTransform.position;
    }
}
