using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGoblinAttack : EnemyAttack
{
    float attackTimer = 0f;
    float attackCooldown = 3f;

    void Update()
    {
        if (canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                StartCoroutine(PerformSwingAttack());
                attackTimer = 0f;
            }
        }
    }
}
