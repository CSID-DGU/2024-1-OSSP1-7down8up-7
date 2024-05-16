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

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Init(float damage, Vector2 dir, float speed)
    {
        this.damage = damage;
        this.per = 1; // Initialize the value

        // Set the bullet velocity
        rigid.velocity = dir.normalized * speed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the game object after the specified lifetime
    }
    private void Update()
    {
        if (per == 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Obstacles"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            per--;
        }

       
    }
}
