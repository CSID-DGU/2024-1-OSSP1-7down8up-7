using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeATKwithNavmesh : MonoBehaviour
{
    Coroutine MoveCoroutine;

    public Rigidbody2D rb2D;
    public CircleCollider2D circleCollider;
    NavMeshAgent agent;
    public GameObject player;

    public bool isFoundAlan = false;

    Vector3 destPosition;
    public float damage;         //공격력
    public float speed;         //속도
    public float health;        //체력
    public float maxHealth;     //최대 체력
    public RuntimeAnimatorController[] animCon;
    SpriteRenderer spriter;
    bool isLive = true;


    public float detectionRange = 5f; // 플레이어 감지 범위
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;

        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WanderRoutine());
    }

 
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                isFoundAlan = true;
                destPosition = player.transform.position;
                Move();  // 목적지 갱신
            }
            else
            {
                isFoundAlan = false;
            }
        }
    }
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!isFoundAlan)
            {
                    ChooseNewdestPosition();
                
                yield return new WaitForSeconds(2f);
            }
            else
            {
                if (IsAtDestination())
                {
                    destPosition = player.transform.position;  // 플레이어 위치 갱신
                    Move();
                }
                yield return new WaitForSeconds(0.1f); // 플레이어 추적 간격을 짧게 설정
            }
        }
    }
    bool IsAtDestination()
    {
        // NavMeshAgent의 현재 위치와 목적지 사이의 거리를 확인
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath;
    }

    void ChooseNewdestPosition()
    {
        Vector3 randomDirection = RandomDirection();
        Vector3 randomDestination = transform.position + randomDirection * Random.Range(1, 3);
        NavMeshHit hit;  // NavMesh 샘플링 결과를 저장할 구조체

        // 최대 2 유닛 범위 내에서 가장 가까운 walkable 위치를 찾음
        if (NavMesh.SamplePosition(randomDestination, out hit, 2.0f, NavMesh.AllAreas))
        {
            destPosition = hit.position;
        }
        else
        {
            // Walkable 위치를 찾지 못한 경우 현재 위치를 유지
            destPosition = transform.position;
        }
        Move();
    }

    Vector3 RandomDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
    public void Move()
    {
        if (isFoundAlan)
        {
            agent.stoppingDistance = 0.3f;
        }
        else
        {
            agent.stoppingDistance = 0.7f;
        }
        agent.SetDestination(destPosition);
    }


    /////////////////////////여기서부터 공격관련

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
            //GameManager.instance.kill++;
            //GameManager.instance.GetExp();
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}



