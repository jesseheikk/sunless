using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CharacterAttack : CharacterBase
{
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] protected float attackRange = 7f;
    [SerializeField] protected VisualEffect swingAttackVFX;
    [SerializeField] protected AudioSource baseAttackSound;

    protected bool canAttack = true;

    public void DisableAttack()
    {
        canAttack = false;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    protected List<GameObject> GetHitObjectsInSector(float angle = 180f)
    {
        List<GameObject> hitObjects = new List<GameObject>();

        // Get the layer of the current game object to prevent ray from hitting itself
        int layerToIgnore = gameObject.layer;
    
        int deadCharacterLayer = LayerMask.NameToLayer("Ghost");
        // Create a layer mask that ignores both the current layer and the "DeadCharacter" layer
        int layerMaskToIgnore = ~(1 << layerToIgnore | 1 << deadCharacterLayer);
    
        Vector3 direction = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        for (float currentAngle = -angle / 2; currentAngle <= angle / 2; currentAngle += angle / 10)
        {
            Quaternion rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
            Vector3 rayDirection = rotation * direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, attackRange, layerMaskToIgnore);
            Debug.DrawRay(transform.position, rayDirection * attackRange, Color.red, 10f);
            if (hit.collider != null && !hitObjects.Contains(hit.collider.gameObject))
            {
                hitObjects.Add(hit.collider.gameObject);
            }
        }

        return hitObjects;
    }

    protected void SpawnProjectile(GameObject projectilePrefab)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectileInstance.transform.localScale = transform.localScale;
        //Projectile projectile = projectileInstance.GetComponent<Projectile>();
    }
}
