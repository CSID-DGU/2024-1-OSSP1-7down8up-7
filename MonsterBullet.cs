using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public enum AttackType {
    HEALTH_DECREASE,
    SPEED_DECREASE,
    DAMAGE_DECREASE,
}
public class MonsterBullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeTime;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;
    public AttackType attackType;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero;
        boxCollider2D = GetComponent<BoxCollider2D>();
        gameObject.tag = "MonsterBullet";
    }

    void Start()
    {
        StartCoroutine(DeactivateAfterTime());// 지정된 lifeTime 후에 게임 오브젝트 제거
    }
    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        rigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    void Update()
    {
        // 매 프레임마다 불릿을 위로 이동
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        // "Wall"이나 "Obstacles" 태그가 있는 오브젝트와 충돌 처리
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Obstacles")|| collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
