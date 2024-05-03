using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Animator anim;

    Rigidbody2D rb;
    Collider2D coll;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);
        anim.SetBool("isMove", inputX != 0 || inputY != 0);
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // 근거리 몬스터와 접촉했을 때
        {
            GameManager.instance.health -= collision.GetComponent<Enemy>().damage; 
            Debug.Log("몬스터와 접촉 ! " + GameManager.instance.health);
        }
        
        if ( GameManager.instance.health > 0)
        {
            // .. 아직 살아있음 -> Hit Action 
        }
        else
        {
            // .. 체력이 0보다 작음 -> Die 
            Dead();
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    void Dead()
    {
        GameManager.instance.GameOver();
    }
}

