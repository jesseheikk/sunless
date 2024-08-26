using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform originalTransform;
    float shakeDuration = 0f;
    float shakeMagnitude = 0.1f;
    float dampingSpeed = 1.0f;

    void Awake()
    {
        originalTransform = transform;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = originalTransform.localPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = originalTransform.localPosition;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
