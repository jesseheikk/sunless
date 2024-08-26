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
            // Instantiate the dropPrefab at the enemy's position and rotation
            GameObject drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody2D component for applying force
            Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();

            // Calculate a random angle between 30 and 80 degrees
            float angle = Random.Range(30f, 300f);

            // Convert the angle to radians for trigonometric calculations
            float angleInRadians = angle * Mathf.Deg2Rad;

            // Calculate the force vector based on the angle
            Vector2 force = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            // Apply the force to the Rigidbody2D to make the dropPrefab fly
            rb.AddForce(force * 10f, ForceMode2D.Impulse); // Adjust the multiplier to control the force strength
        }
    }
}
