using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public MonsterAdapter monsterAdpt;
    public MonsterState AIState;
    public SpawnPoint spawnPoint;
    public Status status;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private CharacterController controller;
    private bool isAtDestination;
    private AttackArea OrcWeapon;
    private Animator animator;
    private Rigidbody2D rb2D;

    private bool IsAttacking;
    private bool IsDied;

    private Vector3 movingDirection;
    private Vector3 nextMovingDirection;
    public Vector3 desireVelocity;

    private static WaitForSeconds CheckingTime = new WaitForSeconds(0.2f);

    private const float GroundCheckDistance = 0.1f;

    // 공격 거리
    public float attackDistance = 1.5f;
    // 플레이어 탐지 거리
    public const float detectionDistance = 5.0f;

    // Roaming , Idle을 반복하는 시간을 재기 위한 타이머
    private const float RoamingTime = 6.0f;
    private const float Idletime = 3.0f;
    private float RoamingTimer;

    // 죽은 몬스터가 사라지는데 걸리는 시간
    private float monsterDisappearingTime = 3f;

    // 애니메이션에 따른, 이동 속도변화에 필요한 상수들
    private const float RoamingSpeedMultiplier = 2.0f;
    private const float ChasingSpeedMultiplier = 2.5f;

    // 공격 전, 랜덤한 시간 동안 기다림
    private bool IsWaiting;
    private float randomWaitingTime;
    private float randomWaitingTime_Timer;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Awake()
    {
        // MonsterAdapter 컴포넌트 추가 및 초기화
        if (monsterAdpt == null)
        {
            monsterAdpt = gameObject.AddComponent<MonsterAdapter>();
        }

        // Status 컴포넌트 추가 및 초기화
        if (status == null)
        {
            status = gameObject.AddComponent<Status>();
        }

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
        isAtDestination = IsAtDestination();
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        controller = GetComponent<CharacterController>();
        status = GetComponent<Status>();
        OrcWeapon = GetComponentInChildren<AttackArea>();
    }

    private void OnEnable()
    {
        monsterAdpt = GetComponent<MonsterAdapter>();
        status = GetComponent<Status>();
        agent.ResetPath();
        IsDied = false;
        status.StatusInit(monsterAdpt.monster.MaxHP);
        AIState = MonsterState.Idle;
        RoamingTimer = 0;
        StartCoroutine(this.CheckMonsterAI());
    }

    IEnumerator CheckMonsterAI()
    {
        while (IsDied == false)
        {
            // 몬스터의 AI 상태는 일정 시간 (CheckingTime) 을 두고 변화함.
            yield return CheckingTime;

            float distanceToPlayer = Vector3.Distance(playerTr.position, monsterTr.position);

            // 플레이어가 탐지 거리 내로 들어오면 추적 시작
            if ((distanceToPlayer < detectionDistance))
            {
                AIState = MonsterState.Chasing;
            }

            // 플레이어가 공격 거리 내로 들어오면 공격 시작
            else if (distanceToPlayer < attackDistance)
            {
                AIState = MonsterState.Attacking;
            }

            // 플레이어와 먼 거리에 있다면, Idle 상태로 (3초) 대기하다, RoamingTime 만큼 (6초) Patrol Area를 랜덤한 방향으로 순찰하는 것을 반복
            else
            {
                if (AIState != MonsterState.Idle)
                {
                    AIState = MonsterState.Roaming;
                }
            }

        }
    }

    private bool IsAtDestination()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath;
    }

    private void update() 
    {
        if (IsDied == true)
        {
            return;
        }

        desireVelocity.x = agent.desiredVelocity.x;
        desireVelocity.y = agent.desiredVelocity.y;

        if (status.CurrentHP <= 0)
        {
            IsDied = true;
            animator.SetBool("IsDied", true);
            Invoke("DeactivateMonster", monsterDisappearingTime);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") |
            animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            desireVelocity.x = 0;
            desireVelocity.y = 0;
        }

        switch (AIState)
        {
            case MonsterState.Attacking:
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") == true)
                    {
                        monsterTr.LookAt(playerTr);
                        agent.SetDestination(playerTr.position);
                    }
                    agent.ResetPath();

                    animator.SetBool("IsAttacking", true);
                    break;
                }
            case MonsterState.Chasing:
                {
                    agent.ResetPath();
                    agent.SetDestination(playerTr.position);

                    monsterTr.LookAt(playerTr);
                    controller.Move(desireVelocity.normalized * Time.deltaTime * monsterAdpt.monster.Speed * ChasingSpeedMultiplier);
                    agent.velocity = controller.velocity;

                    animator.SetBool("IsChasing", true);

                    break;
                }

            case MonsterState.Idle:
                {
                    RoamingTimer += Time.deltaTime;

                    animator.SetBool("IsChasing", false);
                    animator.SetBool("IsIdle", true);

                    if (RoamingTimer > Idletime)
                    {
                        movingDirection = RandomDecideRoamingDirection();
                        nextMovingDirection = RandomDecideRoamingDirection();
                        RoamingTimer = 0;
                        AIState = MonsterState.Roaming;
                    }

                    agent.ResetPath();

                    break;
                }

            case MonsterState.Roaming:
                {
                    RoamingTimer += Time.deltaTime;

                    animator.SetBool("IsChasing", false);
                    animator.SetBool("IsIdle", false);

                    if (RoamingTimer > RoamingTime)
                    {
                        RoamingTimer = 0;
                        AIState = MonsterState.Idle;
                    }

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roaming"))
                    {
                        monsterTr.LookAt(movingDirection);
                        agent.ResetPath();
                        agent.SetDestination(movingDirection);

                        controller.Move(desireVelocity.normalized * Time.deltaTime * monsterAdpt.monster.Speed * ChasingSpeedMultiplier);
                        agent.velocity = controller.velocity;

                        if (isAtDestination)
                        {
                            movingDirection = nextMovingDirection;
                        }
                    }

                    animator.SetBool("IsRoaming", true);

                    break;
                }
        }
    }

    private Vector3 RandomDecideRoamingDirection()
    {
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private void DeactivateMonster()
    {
        gameObject.SetActive(false);
    }

    public bool ToggleAttackArea()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            OrcWeapon.OnAttack();
            return true;
        }
        else
        {
            OrcWeapon.OffAttack();
        }

        return false;
    }

    //!!!몬스터 공격받은 이벤트 처리 collision 처리 구현
    public void HandleAttackedEvent(Damage damage)
    {
        if (IsDied == true)
        {
            return;
        }

        //animator.SetTrigger("Damaged");
        transform.LookAt(damage.attacker.transform);
        // if (damage.attacker.GetCurrentAnimatorStateInfo(0).IsTag("DamageAttack"))
        // {
        //     animator.SetTrigger("Damaged");
        //     transform.LookAt(damage.attacker.transform);
        // }

    }

    // Wait 모션이 시작될 때 호출되어 기다릴, 시간을 결정
    private void DecideRandomWaitTIme()
    {
        randomWaitingTime = UnityEngine.Random.Range(2f, 5f);
    }

}
