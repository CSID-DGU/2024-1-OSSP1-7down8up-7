using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float damage;         //공격력
    public float speed;         //속도
    public float health;        //체력
    public float maxHealth;     //최대 체력
    public RuntimeAnimatorController[] animCon;     //몬스터의 애니메이터를 바꾸기 위한 컨트롤러
    public Rigidbody2D target;  //목표 Rigidbody

    bool isLive; //생존여부

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }
    
    void FixedUpdate()
    {
        if (!isLive) //죽었으면 종료
            return;

        Vector2 dirVec = target.position - rigid.position; // 방향 = 위치 차이의 정규화 (위치 차이 = 타겟 위치 - 나의 위치)
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //방향(정규화) * 속도 * 프레임 시간 보정
        rigid.MovePosition(rigid.position + nextVec); //현재 위치에 next벡터를 더한다.
        //다른 리지드바디와 부딪히게 되면 물리 속도가 생기는데 우리는 위치 이동을 채용하고 있으므로 물리 속도로 위치가 변화하면 안되므로 velocity를 0 만들기
        rigid.velocity = Vector2.zero; //물리 속도가 이동에 영향을 주지 않도록 속도는 제거 
    }

    private void LateUpdate()
    {
        if (!isLive) //죽었으면 종료
            return;

        //목표의 x축과 자신의 x축 값을 비교하여 작으면 X축을 기준으로 Flip 되도록 FlipX를 True로 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable() //활성화 될 때 한 번 실행
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true; //생존여부 초기화
        health = maxHealth;
    }

    public void Init(SpawnData data) //초기 속성을 적용하는 함수 작성
    {
        //anim.runtimeAnimatorController = animCon[data.spriteType];  //애니메이션 적용
        damage = data.damage;       //공격력 적용
        speed = data.speed;         //속도 적용
        maxHealth = data.health;    //체력 적용
        health = data.health;                                           
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) // 충돌한 collision이 Bullet인지를 먼저 확인
            return;

        health -= collision.GetComponent<Bullet>().damage; //Bullet 스크립트 컴포넌트에서 damage를 가져와서 체력에서 깍는다.
        Debug.Log("저격 성공 ! ");

        if (health > 0)
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
        gameObject.SetActive(false);
    }
}
