using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeBodyController : SnakePartController
{
    private NavMeshAgent agent;
    private Transform playerTr;

    private MonsterStat stat;
    private MonsterShooter monsterShooter;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindWithTag("Player").transform;
        stat = GetComponent<MonsterStat>();
        monsterShooter = GetComponent<MonsterShooter>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        agent.speed = stat.GetMoveSpeed();
        agent.stoppingDistance = 1f;
    }

    private void Update()
    {
        if (!stat.IsAlive())
        {
            Destroy(gameObject);
            return;
        }

        if (agent.isOnNavMesh && previousBodyPart != null)
        {
            agent.SetDestination(previousBodyPart.transform.position);

            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }

        if (Vector3.Distance(playerTr.position, transform.position) <= stat.GetAttackDistance()) //5f
        {
            monsterShooter.Shoot();
        }
    }
}
