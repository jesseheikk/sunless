using System.Collections;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    [SerializeField] AudioSource pickUpSound;
    [SerializeField] float pulseDuration = 1f;
    [SerializeField] float pulseScaleFactor = 1.2f;
    [SerializeField] float destroyTime = 0.5f;
    [SerializeField] bool isPersistent = true;

    Vector3 originalScale;
    ParticleSystem pickUpParticles;
    Animator animator;
    Rigidbody2D rb;

    string collectableID;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pickUpParticles = GetComponentInChildren<ParticleSystem>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on the collectable prefab.");
        }

        if (isPersistent)
        {
            // For persistent collectables
            if (string.IsNullOrEmpty(collectableID))
            {
                collectableID = PlayerPrefs.GetString(gameObject.name + "_UUID", Guid.NewGuid().ToString());
                PlayerPrefs.SetString(gameObject.name + "_UUID", collectableID);
            }
        }
        else
        {
            // For runtime collectables spawned during gameplay
            collectableID = Guid.NewGuid().ToString();
        }

        Debug.Log(collectableID);
    }

    void Start()
    {
        // Check if the item has already been collected (only for persistent collectables)
        if (isPersistent && PlayerInfo.HasCollectedItem(collectableID))
        {
            gameObject.SetActive(false); // Disable the collectable
        }
        else
        {
            originalScale = transform.localScale;
            StartCoroutine(Pulse());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Prevent from falling through ground
        if (collider.CompareTag("Ground"))
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
            else
            {
                Debug.LogWarning("Rigidbody2D is not attached to the collectable.");
            }
        }

        if (collider.CompareTag("Player"))
        {
            PickUp(collider.gameObject);
        }
    }

    protected virtual void PickUp(GameObject player)
    {
        if (animator)
        {
            animator.SetTrigger("PickUp");
        }

        if (pickUpSound)
        {
            pickUpSound.Play();
        }

        if (pickUpParticles)
        {
            pickUpParticles.Play();
        }

        // Add to the list of collected items (only for persistent collectables)
        if (isPersistent)
        {
            PlayerInfo.AddCollectedItem(collectableID);
        }

        Destroy(gameObject, destroyTime);
    }

    IEnumerator Pulse()
    {
        while (true)
        {
            // Scale up
            yield return StartCoroutine(ScaleTo(originalScale * pulseScaleFactor, pulseDuration / 2));
            // Scale down
            yield return StartCoroutine(ScaleTo(originalScale, pulseDuration / 2));
        }
    }

    IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure it reaches the target scale
    }
}
