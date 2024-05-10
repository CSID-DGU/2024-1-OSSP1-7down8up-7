using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 몇 명이나 맞출 수 있는지
    public float speed;
    public float lifeTime;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Init(float damage, Vector3 dir)
    {
        this.damage = damage;
        this.per = 1; // 이 값은 초기화에서 결정되어야 함.

        // 설정된 방향으로 불릿 속도 설정
        rigid.velocity = dir * 15f;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); // 지정된 lifeTime 후에 게임 오브젝트 제거
    }

    void Update()
    {
        // 매 프레임마다 불릿을 위로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit something: " + collision.gameObject.name);

        // "Wall"이나 "Obstacles" 태그가 있는 오브젝트와 충돌 처리
        if (collision.CompareTag("Wall") || collision.CompareTag("Obstacles"))
        {
            Debug.Log("Hit a wall");
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy");
            per--;
            // 적에게 데미지를 주는 로직이 필요하면 여기에 추가
        }

        // 불릿이 맞출 수 있는 횟수 감소
       

        // 불릿이 더 이상 맞출 수 없으면 비활성화
        if (per < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
