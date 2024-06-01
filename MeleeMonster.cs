using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static PassiveItem;

public enum MONSTER_MELEE
{
    Spagetti,
}

public class MeleeMonster : Monster
{
    private MonsterStat stat;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public void Awake() 
    {
        IsAttacking = false;
        stat = GetComponent<MonsterStat>();
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        monsterController = GetComponent<MonsterController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.avoidancePriority = UnityEngine.Random.Range(51, 100);
        animator = GetComponent<Animator>();
    }

    public override void Attack() 
    {
        //player stat changed ! 
       
        IsAttacking = false;
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�ƾ�!" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Bullet")) //monster is shot
        {
            stat.GetHarmd(collision.gameObject.GetComponent<Bullet>().damage);
            animator.SetTrigger("isDamaged");
            //Debug.Log("�ƾ�!");
            StartCoroutine(BlinkRed());
        }
        return;
    }

    private IEnumerator BlinkRed()
    {
        // Debug.Log("����");
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
}