using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeTime;

    Rigidbody2D rigid;
    public Rigidbody2D target;  //목표 Rigidbody
    Collider2D coll;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    public void Init(float damage) //int per 임시 삭제
    {
        this.damage = damage;
    }

    void Start() 
    {
        Vector2 dirVec = target.position - rigid.position; 
        GetComponent<Rigidbody2D>().AddForce(dirVec.normalized * Time.deltaTime * 10000);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))  
            return;
        
        GameManager.instance.health -= damage; 
        Debug.Log("원거리 몬스터 공격 ! " + GameManager.instance.health);
        
        gameObject.SetActive(false);
    }
}

