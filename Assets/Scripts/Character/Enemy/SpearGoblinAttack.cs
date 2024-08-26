using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGoblinAttack : EnemyAttack
{
    [SerializeField] GameObject projectilePrefab;

    float attackTimer = 0f;
    float attackCooldown = 3f;

    void Update()
    {
        if (canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                StartCoroutine(PerformProjectileAttack(projectilePrefab));
                attackTimer = 0f;
            }
        }
    }
}
