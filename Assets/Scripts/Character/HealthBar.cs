using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float widthPerHealthUnit = 2f;

    Slider slider;
    RectTransform rectTransform;
    Vector2 initialPosition;

    void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();

        // Store the initial anchored position
        initialPosition = rectTransform.anchoredPosition;

        // Set pivot to the left center
        rectTransform.pivot = new Vector2(0, 0.5f);

        // Set the anchor to the left so the position remains fixed when resizing
        rectTransform.anchorMin = new Vector2(0, 0.5f);
        rectTransform.anchorMax = new Vector2(0, 0.5f);
    }

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
        AdjustWidth(value);
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }

    private void AdjustWidth(float maxHealth)
    {
        //float newWidth = baseWidth + (maxHealth * widthPerHealthUnit);
        float newWidth = maxHealth * widthPerHealthUnit;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);

        // Reset the anchored position to the initial value to prevent shifting
        rectTransform.anchoredPosition = initialPosition;
    }
}
