using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    private Monster monster;
    private MonsterState AIState;
    private MonsterStat stat;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    private bool isIdle;
    private bool IsAttacking;
    private bool IsDied;

    private Vector3 movingDirection;
    private Vector3 nextMovingDirection;

    private static readonly WaitForSeconds CheckingTime = new WaitForSeconds(0.3f);

    private const float WanderingTime = 6.0f;
    private const float IdleTime = 1.0f;
    private float WanderingTimer;
    private float monsterDisappearingTime = 1f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        switch (gameObject.tag)
        {
            case "MeleeMonster":
                monster = GetComponent<MeleeMonster>();
                break;
            case "RangedMonster":
                monster = GetComponent<RangedMonster>();
                break;
            case "DebuffMonster":
                monster = GetComponent<DebuffMonster>();
                break;
        }

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        stat = GetComponent<MonsterStat>();
        monsterTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        WanderingTimer = 0f;
        IsDied = false;
        AIState = MonsterState.Idle;
        animator.SetBool("isIdle", true);    
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stat.GetMoveSpeed();
        agent.stoppingDistance = stat.GetAttackDistance() - 0.2f;
        //agent.ResetPath();
        StartCoroutine(CheckMonsterAI());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckMonsterAI()
    {
        while (!IsDied)
        {
            yield return CheckingTime;

            float DistanceFromPlayer = Vector3.Distance(playerTr.position, monsterTr.position);
            monster.rb2D.isKinematic = true;

            if (monster.IsAttacking || DistanceFromPlayer < stat.GetAttackDistance())
            {
                if (DistanceFromPlayer == 0)
                {
                    monster.rb2D.isKinematic = false;
                }
                AIState = MonsterState.Attacking;   
            }
            else if (DistanceFromPlayer < stat.GetDetectionDistance())
            {
                AIState = MonsterState.Chasing;  
            }
            else
            {
                if (AIState != MonsterState.Idle)
                {
                    AIState = MonsterState.Wandering;    
                }
            }
        }
    }

    private void Update()
    {
        if (!stat.IsAlive())
        {
            DeactivateMonster();
            return;
        }
        if (agent.isOnNavMesh)
        {
            switch (AIState)
            {
                case MonsterState.Attacking:
                    animator.SetBool("isAttacking", true); 
                    HandleAttackingState();
                    break;
                case MonsterState.Chasing:
                    animator.SetBool("isChasing", true);  
                    HandleChasingState();
                    break;
                case MonsterState.Idle:
                    animator.SetBool("isIdle", true);    
                    HandleIdleState();
                    break;
                case MonsterState.Wandering:
                    animator.SetBool("isWandering", true);
                    HandleWanderingState();
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        if (playerTr.position.x >= this.transform.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }


    private void HandleAttackingState()
    {
        agent.ResetPath();
        agent.SetDestination(playerTr.position);
        monster.Attack();
        //Debug.Log(monster.name + " Attack call");
    }

    private void HandleChasingState()
    {
        agent.SetDestination(playerTr.position);
    }

    private void HandleIdleState()
    {
        if (agent.isOnNavMesh)
        {
            WanderingTimer += Time.deltaTime;
            if (WanderingTimer > IdleTime)
            {
                Vector3 randomDirection = RandomDirection();
                Vector3 randomDestination = transform.position + randomDirection * Random.Range(1, 3);
                NavMeshHit hit;  // NavMesh 샘플링 결과를 저장할 구조체

                if (NavMesh.SamplePosition(randomDestination, out hit, 2.0f, NavMesh.AllAreas))
                {
                    movingDirection = hit.position;
                }
                else
                {
                    // Walkable 위치를 찾지 못한 경우 현재 위치를 유지
                    movingDirection = transform.position;
                }

                WanderingTimer = 0;
                AIState = MonsterState.Wandering;
            }

            agent.ResetPath();
        }
            
    }

    private void HandleWanderingState()
    {
        WanderingTimer += Time.deltaTime;

        if (WanderingTimer > WanderingTime)
        {
            WanderingTimer = 0;
            AIState = MonsterState.Idle;
        }

        agent.SetDestination(movingDirection);

        if (isAtTargetLocation())
        {
            movingDirection = nextMovingDirection;
        }
    }

    private Vector3 RandomDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private void DeactivateMonster()
    {
        gameObject.SetActive(false);
        gameObject.GetComponentInParent<MonsterManager>().RemoveMonster(monster);
        Destroy(gameObject);
    }


    private bool isAtTargetLocation()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath;
    }
}
