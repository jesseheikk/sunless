using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : CharacterHealth
{
    CameraShake cameraShake;
    ParticleSystem damageParticles;
    [SerializeField] Text deathText;

    protected override void Start()
    {
        UpdateMaxHealth();
        base.Start();

        cameraShake = Camera.main.GetComponent<CameraShake>();
        if (cameraShake == null)
        {
            Debug.LogWarning("CameraShake script not found on the main camera.");
        }

        damageParticles = GetComponentInChildren<ParticleSystem>();
        if (damageParticles == null)
        {
            Debug.LogWarning("Damage Particle System not found in child objects.");
        }

        GameObject deathTextObject = GameObject.Find("DeathText");
        if (deathTextObject != null)
        {
            deathText = deathTextObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("DeathText GameObject not found.");
        }
    }

    public void UpdateMaxHealth()
    {
        maxHealth = PlayerInfo.maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public override IEnumerator TakeDamage(float damageAmount, Transform damagingObject)
    {
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        if (cameraShake != null)
        {
            cameraShake.Shake(pauseDurationOnDamage, 0.3f);
        }

        if (damageParticles != null)
        {
            damageParticles.Play();
        }

        PlayerAttack playerAttack = GetComponent<PlayerAttack>();
        playerAttack.DisableAttack();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.DisableMovement();

        yield return base.TakeDamage(damageAmount, damagingObject);

        PlayerInfo.currentHealth = currentHealth;

        playerAttack.EnableAttack();
        playerMovement.EnableMovement();
        yield return new WaitForSeconds(0.5f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    protected override void Die()
    {
        base.Die();
        deathText.gameObject.SetActive(enabled);
        StartCoroutine(ReloadSceneAfterDelay(5f));
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
