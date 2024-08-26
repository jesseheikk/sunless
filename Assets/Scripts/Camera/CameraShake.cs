using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform originalTransform;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1.0f;

    private void Awake()
    {
        originalTransform = transform;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    private void Update()
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
}
