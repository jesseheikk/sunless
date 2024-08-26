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

        // Set the position with min and max values to keep the left starting point
        // static when the healthbar grows in size
        initialPosition = rectTransform.anchoredPosition;
        rectTransform.pivot = new Vector2(0, 0.5f);
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
        float newWidth = maxHealth * widthPerHealthUnit;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        rectTransform.anchoredPosition = initialPosition;
    }
}
