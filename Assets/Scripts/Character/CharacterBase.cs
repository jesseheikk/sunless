using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Turns on/off all scripts attached to the same gameobject excluding itself
    protected void ToggleOtherScripts(bool enable = false)
    {
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = enable;
            }
        }
    }

    protected void PushAwayFromObject(Transform interactedObject, float pushForce)
    {
        if (rb == null) return;

        Vector2 pushDirection = new Vector2(transform.position.x - interactedObject.position.x, 0).normalized;
        Vector2 force = pushDirection * pushForce;

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
