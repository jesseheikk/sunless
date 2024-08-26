using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    [SerializeField] bool damageOtherEnemiesOnCollision = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {   
            if (playerHealth.vulnerable)
            {
                StartCoroutine(InflictDamage(playerHealth));
            }
        }
        else if (damageOtherEnemiesOnCollision)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                StartCoroutine(enemyHealth.TakeDamage(attackDamage, transform));
            }
        }
    }

    protected IEnumerator PerformProjectileAttack(GameObject projectilePrefab)
    {
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.DisableMovement();
        animator.SetTrigger("ThrowAttack");
        yield return new WaitForSeconds(1f);
        SpawnProjectile(projectilePrefab);
        enemyMovement.EnableMovement();
    }

    protected IEnumerator PerformSwingAttack()
    {
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.DisableMovement();

        animator.SetTrigger("SwingAttack");
        yield return new WaitForSeconds(1f);
        swingAttackVFX?.Play();
        //baseAttackSound?.Play();

        List<GameObject> hitObjects = GetHitObjectsInSector();
        foreach (GameObject hitObject in hitObjects)
        {
            if (hitObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hitObject.GetComponent<PlayerHealth>();
                yield return StartCoroutine(playerHealth.TakeDamage(attackDamage, transform));
            }
        }
        enemyMovement.EnableMovement();
    }

    protected IEnumerator InflictDamage(PlayerHealth playerHealth)
    {
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement)
        {
            enemyMovement.DisableMovement();
        }

        yield return playerHealth.TakeDamage(attackDamage, transform);

        if (enemyMovement)
        {
            enemyMovement.EnableMovement();
        }
    }
}
