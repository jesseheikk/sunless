using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float damage = 10f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wallCheckDistance = 3f;
    [SerializeField] float maxLifeTime = 5f;

    bool collided = false;
    float lifeTimer = 0f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!collided)
        {
            lifeTimer += Time.deltaTime;

            if (lifeTimer > maxLifeTime)
            {
                Destroy(gameObject);
            }

            float localScaleX = transform.localScale.x > 0 ? 1f : -1f;
            Vector2 direction = Vector2.right * localScaleX;

            transform.Translate(direction * movementSpeed * Time.deltaTime);

            if (IsWallAhead(direction))
            {
                collided = true;
                animator.SetTrigger("Impact");
                Destroy(gameObject, 0.7f);
            }
        }


    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collided)
        {
            return;
        }

        PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {   
            if (playerHealth.vulnerable)
            {
                collided = true;
                StartCoroutine(HandlePlayerCollision(playerHealth));
            }
        }
    }

    protected bool IsWallAhead(Vector2 direction)
    {
        // Adjust the ray start position based on the direction
        float rayStartOffset = 0.1f;
        Vector2 rayStart = (Vector2)transform.position + direction * rayStartOffset;

        // Cast the ray in the specified direction
        RaycastHit2D hit = Physics2D.Raycast(rayStart, direction, wallCheckDistance, groundLayer);

        // Return true if a collider is hit, false otherwise
        return hit.collider != null;
    }

    IEnumerator HandlePlayerCollision(PlayerHealth playerHealth)
    {
        animator.SetTrigger("Impact");
        yield return playerHealth.TakeDamage(damage, transform);
        Destroy(gameObject);
    }
}