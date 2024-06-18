using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public enum MONSTER_DEBUFF
{
    Ghost,
}

public class DebuffMonster : Monster
{
    private MonsterStat stat;
    private MonsterShooter monsterShooter;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private bool isKnockedBack;
    private Vector2 knockbackDirection;
    private float knockbackEndTime;
    private float knockbackForce = 5f; // 넉백 힘 설정

    public void Awake()
    {
        stat = GetComponent<MonsterStat>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.avoidancePriority = UnityEngine.Random.Range(51, 100);
        IsAttacking = false;
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        monsterController = GetComponent<MonsterController>();
        monsterShooter = GetComponent<MonsterShooter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        // 넉백 관련
        if (isKnockedBack)
        {
            // Debug.Log("넉백됨");
            rb2D.velocity = knockbackDirection * knockbackForce;

            // 넉백 시간이 끝나면 상태 초기화
            if (Time.time >= knockbackEndTime)
            {
                isKnockedBack = false;
                rb2D.velocity = Vector2.zero;
            }
        }
    }

    public override void Attack()
    {
        monsterShooter.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(" ƾ !" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Bullet")) //monster is shot
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            PlayerStat playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= playerStat.GetCritical())
            {
                stat.GetMonsterHarmd(collision.gameObject.GetComponent<Bullet>().damage * 2);
                animator.SetTrigger("isDamaged");
                //Debug.Log(" ƾ !");
                ApplyKnockback(direction, BlinkBlue());
            }
            else
            {
                stat.GetMonsterHarmd(collision.gameObject.GetComponent<Bullet>().damage);
                animator.SetTrigger("isDamaged");
                //Debug.Log(" ƾ !");
                ApplyKnockback(direction, BlinkRed());
            }

        }
        return;
    }

    private void ApplyKnockback(Vector2 direction, IEnumerator BlinkFunc)
    {
        isKnockedBack = true;
        knockbackDirection = direction;
        knockbackEndTime = Time.time + 0.08f; // 넉백 지속 시간 설정
        StartCoroutine(BlinkFunc); // 넉백과 동시에 깜빡임 효과 시작
    }

    private IEnumerator BlinkRed()
    {
        // Debug.Log("    ");
        float blinkDuration = 0.5f; // Total duration of the blinking effect
        float blinkInterval = 0.1f; // Interval between blinks
        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += 2 * blinkInterval;
        }
        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = originalColor;
    }
    private IEnumerator BlinkBlue()
    {
        // Debug.Log("    ");
        float blinkDuration = 0.5f; // Total duration of the blinking effect
        float blinkInterval = 0.1f; // Interval between blinks
        float elapsedTime = 0f;
        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = Color.blue;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += 2 * blinkInterval;
        }
        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = originalColor;
    }
}