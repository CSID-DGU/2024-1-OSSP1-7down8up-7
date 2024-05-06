using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Animator anim;
    bool door_check0 = false;
    bool door_check1 = false;
    bool door_check2 = false;   // door tag에 숫자 부여하여 door1, door2과 같은 형식으로 표현 
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

        Vector3 pos = this.transform.position;
        Vector3 pos1 = new Vector3(7.5f, 0.0f, pos.z);
        float term = 0.5f;
        bool door_check = (pos.x < pos1.x + term) && (pos.x > pos1.x - term) && (pos.y < pos1.y + term) && (pos.y > pos1.y - term);
        if (door_check0)
        {
            SceneManager.LoadScene("CombatScene1");
            Debug.Log("door0");
        }

        if (door_check1)
        {
            SceneManager.LoadScene("CombatScene2");
            Debug.Log("door1");
        }
        if(door_check2)
        {
            SceneManager.LoadScene("CombatScene3");
            Debug.Log("door2");
        } 
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
        else if (collision.CompareTag("door0"))
        {
            door_check0 = true;
        }
        else if (collision.CompareTag("door1"))
        {
            door_check1 = true;
        }
        else if (collision.CompareTag("door2"))
        {
            door_check2 = true;
        }
        // door_check 변수가 위치 기반으로 되어 있는데 door object에 collider 감지되면 TRUE로 바뀌도록

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

