using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterHealth : CharacterBase
{
    [SerializeField] protected float maxHealth = 10f;
    [SerializeField] protected float pushForceOnDamage = 20f;
    [SerializeField] protected float pauseDurationOnDamage = 1f;
    [SerializeField] protected HealthBar healthBar;

    Color originalColor;
    UnityEngine.Rendering.Universal.Light2D characterLight;
    float dimDuration = 2f;
    float dimIntensity = 0.2f;

    [HideInInspector] public bool vulnerable = true;
    protected float currentHealth;

    protected override void Start()
    {
        base.Start();
        if (healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(maxHealth);
        }

        currentHealth = maxHealth;

        characterLight = GetComponentInChildren<Light2D>();
        if (!characterLight)
        {
            Debug.LogWarning("Light2D component not found in children.");
        }

        originalColor = spriteRenderer.color;
    }

    public virtual IEnumerator TakeDamage(float damageAmount, Transform damagingObject)
    {
        vulnerable = false;

        currentHealth -= damageAmount;
        if (healthBar)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            StartCoroutine(FlashSpriteRed());
            PushAwayFromObject(damagingObject, pushForceOnDamage);
            yield return new WaitForSeconds(pauseDurationOnDamage);
            vulnerable = true;
        }
    }

    protected virtual void Die()
    {
        if (animator)
        {
            animator.SetTrigger("Die");
        }

        StopAllCoroutines();
        spriteRenderer.color = originalColor;
        // Prevent collision with other objects
        gameObject.layer = LayerMask.NameToLayer("Ghost");

        ToggleOtherScripts();
        StartCoroutine(DimLight());
        this.enabled = false;
    }

    IEnumerator FlashSpriteRed()
    {
        Color flashColor = Color.red;
        float elapsedTime = 0f;

        while (elapsedTime < pauseDurationOnDamage)
        {
            // Toggle between original color and flash color
            spriteRenderer.color = (spriteRenderer.color == originalColor) ? flashColor : originalColor;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }

        // Ensure the sprite's color is back to its original color
        // This can happen if the coroutine is stopped early from other scripts, etc.
        spriteRenderer.color = originalColor;
    }

    IEnumerator DimLight()
    {
        if (characterLight)
        {
            float originalIntensity = characterLight.intensity;
            float elapsedTime = 0f;

            while (elapsedTime < dimDuration)
            {
                float t = elapsedTime / dimDuration;
                characterLight.intensity = Mathf.Lerp(originalIntensity, dimIntensity, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Ensure the light is fully dimmed
            characterLight.intensity = dimIntensity;
        }
    }
}
