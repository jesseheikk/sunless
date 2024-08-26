using UnityEngine;

public class GhostFade : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1f;

    SpriteRenderer spriteRenderer;
    float fadeTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        fadeTimer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
        Color newColor = spriteRenderer.color;
        newColor.a = alpha;
        spriteRenderer.color = newColor;

        if (alpha <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

