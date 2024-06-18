using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public float speed;
    public float lifeTime;

    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2D;
    private Animator animator;

    public Sprite origin;

    void Awake()
    {
   
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void Init(float damage, Vector2 dir, float speed,float lifetime)
    {

        
        this.damage = damage;
        this.per = 1; // Initialize the value
        this.lifeTime = lifetime;
        

        // Set the bullet velocity
        rigid.velocity = dir.normalized * speed;

        // Start the deactivation coroutine based on the bullet's lifeTime
        StartCoroutine(DeactivateAfterTime());
        
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        rigid.velocity = Vector3.zero;
        GetComponent<SpriteRenderer>().sprite = origin;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Obstacles"))
        {
            StartCoroutine(Deactivate());
        }
        else if (collision.CompareTag("MeleeMonster") || collision.CompareTag("RangedMonster") || collision.CompareTag("DebuffMonster") || collision.CompareTag("BossMonster"))
        {
            per--;
            if (per == 0)
            {
                StartCoroutine(Deactivate());
            }
        }
    }

    private IEnumerator Deactivate()
    {
        rigid.velocity = Vector3.zero; // 탄환 멈추기
        animator.SetTrigger("Deactive"); // Deactive 트리거 설정
        yield return new WaitForSeconds(0.15f);
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }
        GetComponent<SpriteRenderer>().sprite = origin;
        gameObject.SetActive(false); // 오브젝트 비활성화
    }
}