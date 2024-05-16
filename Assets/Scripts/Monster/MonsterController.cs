using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//manage monster's movement and state
public class MonsterController : MonoBehaviour
{
    //monster object
    private Monster monster;

    //state
    private MonsterState AIState;

    //transform
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;

    //check state
    private bool isIdle;
    private bool IsAttacking;
    private bool IsDied;

    //moving direction
    private Vector3 movingDirection;
    private Vector3 nextMovingDirection;
    public Vector3 desireVelocity;

    private static WaitForSeconds CheckingTime = new WaitForSeconds(0.2f);

    //distance from player //store in monster script
    float attackDistance;
    float detectionDistance;

    // when monster die, disappearing time
    private float monsterDisappearingTime = 1f;

    private void Awake()
    {
        switch (gameObject.tag)
        {
            case "MeleeMonster":
                monster = GetComponent<MeleeMonster>();
                Debug.Log("meleeMonster controller");
                break;
            case "RangedMonster":
                monster = GetComponent<RangedMonster>();
                Debug.Log("rangedMonster controller");
                break;
            // case "DebuffMonster":
            //     monster = GetComponent<debuffMonster>();
            //     break;
            default:
                Debug.LogError("Unknown monster type on: " + gameObject.name);
                break;
        }
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackDistance;
    }

    private void OnEnable()
    {
        agent.ResetPath();
        IsDied = false;
        AIState = MonsterState.Idle;
    }

    private void Start()
    {
        attackDistance = monster.attackDistance;
        detectionDistance = monster.detectionDistance;
        StartCoroutine(this.CheckMonsterAI());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    //checking monster state using distance
    IEnumerator CheckMonsterAI()
    {
        while (IsDied == false)
        {
            yield return CheckingTime;

            float DistanceFromPlayer = Vector3.Distance(playerTr.position, monsterTr.position);

            if (DistanceFromPlayer < detectionDistance && DistanceFromPlayer >= attackDistance)
            {
                AIState = MonsterState.Chasing;
            }
            else if (DistanceFromPlayer < attackDistance)
            {
                AIState = MonsterState.Attacking;
            }
            else
            {
                AIState = MonsterState.Idle;
            }
        }
    }

    private void Update()
{
    if (IsDied)
    {
        return;
    }

    desireVelocity.x = agent.desiredVelocity.x;
    desireVelocity.y = agent.desiredVelocity.y;

    if (monster.CurrentHP <= 0)
    {
        IsDied = true;
        Invoke("DeactivateMonster", monsterDisappearingTime);
        return;
    }

    // Action Change by AI State //monster polymorphism
    switch (AIState)
    {
        case MonsterState.Attacking:
            {
                monsterTr.LookAt(playerTr);
                agent.ResetPath();

                monster.Attack(); //monster's method call

                break;
            }
        case MonsterState.Chasing:
            {
                if (!agent.hasPath || agent.remainingDistance < 0.5f)
                {
                    agent.SetDestination(playerTr.position);
                }
                break;
            }
        case MonsterState.Idle:
            {
                if (!agent.hasPath || agent.remainingDistance < 0.5f)
                {
                    movingDirection = RandomDecideRoamingDirection();
                    agent.SetDestination(movingDirection);
                }
                break;
            }
    }
}

    private Vector3 RandomDecideRoamingDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private void DeactivateMonster()
    {
        gameObject.SetActive(false);
    }

    private bool isAtTargetLocation(NavMeshAgent navMeshAgent, Vector3 moveTarget, float minDistance)
    {
        float dist;

        //-- If navMeshAgent is still looking for a path then use line test
        if (navMeshAgent.pathPending)
        {
            dist = Vector3.Distance(transform.position, moveTarget);
        }
        else
        {
            dist = navMeshAgent.remainingDistance;
        }

        return dist <= minDistance;
    }
}
