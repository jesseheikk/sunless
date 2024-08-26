using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    [SerializeField] GameObject dropPrefab;
    [SerializeField] int numberOfDrops = 1;

    public override IEnumerator TakeDamage(float damageAmount, Transform damagingObject)
    {  
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        EnemyAttack enemyAttack = GetComponent<EnemyAttack>();

        enemyMovement.DisableMovement();
        enemyAttack.DisableAttack();

        yield return base.TakeDamage(damageAmount, damagingObject);

        enemyMovement.EnableMovement();
        enemyAttack.EnableAttack();
    }

    protected override void Die()
    {
        base.Die();
        DropItems();
    }

    private void DropItems()
    {
        for (int i = 0; i < numberOfDrops; i++)
        {
            GameObject drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();
            float angle = Random.Range(30f, 300f);

            // Convert the angle to radians for trigonometric calculations
            float angleInRadians = angle * Mathf.Deg2Rad;

            // Calculate the force vector based on the angle
            Vector2 force = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            rb.AddForce(force * 10f, ForceMode2D.Impulse);
        }
    }
}
