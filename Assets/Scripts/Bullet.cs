using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; //몇명이나 맞출 수 있는지
    public float speed;
    public float lifeTime;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
       
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Init(float damage, Vector3 dir) //int per 임시 삭제
    {
        this.damage = damage;
        this.per = 1;
        //this.per = per;

        if (per >= 0) {
            rigid.velocity = dir * 15f;
        }
    }

    void Start() 
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
        if(collision.CompareTag("Wall")|| collision.CompareTag("Obstacles"))
        {
            Debug.Log("벽 충돌");
            Destroy(gameObject);
        }
        

        per--;

        if (per < 0) {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}

