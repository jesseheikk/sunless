using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : CharacterAttack
{
    [SerializeField] float attackRecoilForce = 50.0f;
    [SerializeField] AudioSource wallHitSound;
    [SerializeField] AudioSource enemyHitSound;

    float attackCooldown = 0.5f;
    float lastAttackTime;
    bool attackInputReceived = false;
    Text attackText;

    protected override void Start()
    {
        GameObject attackTextObject = GameObject.Find("AttackText");
        if (attackTextObject != null)
        {
            attackText = attackTextObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("AttackText GameObject not found.");
        }

        UpdateAttack();
        base.Start();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (Time.time - lastAttackTime >= attackCooldown && canAttack)
            {
                attackInputReceived = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (attackInputReceived)
        {
            PerformSwingAttack();
            lastAttackTime = Time.time;
            attackInputReceived = false;
        }
    }

    public void UpdateAttack()
    {
        attackDamage = PlayerInfo.attack;
        attackText.text = attackDamage.ToString();
    }

    void PerformSwingAttack()
    {
        baseAttackSound?.Play();
        swingAttackVFX?.Play();

        List<GameObject> hitObjects = GetHitObjectsInSector(20f);
        foreach (GameObject hitObject in hitObjects)
        {
            HandleHit(hitObject);
        }
    }

    void HandleHit(GameObject hitObject)
    {
        if ((1 << hitObject.layer) == LayerMask.GetMask("Enemy"))
        {
            if (enemyHitSound)
            {
                enemyHitSound.time = 0.2f;
                enemyHitSound.Play();
            }

            EnemyHealth enemyHealth = hitObject.GetComponent<EnemyHealth>();
            StartCoroutine(enemyHealth.TakeDamage(attackDamage, transform));

            PushAwayFromObject(hitObject.transform, attackRecoilForce);
        }
        else if ((1 << hitObject.layer) == LayerMask.GetMask("Ground"))
        {
            if (wallHitSound)
            {
                wallHitSound.Play();
            }

            // Push player backwards
            Vector2 pushDirection = new Vector2(transform.localScale.x * -1, 0).normalized;
            Vector2 force = pushDirection * attackRecoilForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
        else if ((1 << hitObject.layer) == LayerMask.GetMask("BreakableObject"))
        {
            BreakableObject breakableObject = hitObject.GetComponent<BreakableObject>();
            breakableObject.Break();
        }
    }
}