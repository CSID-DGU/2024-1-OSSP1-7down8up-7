using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    private Skeleton monster;


    private Transform playerTr;
    private Transform monsterTr;

    private NavMeshAgent agent;
    private MonsterStat stat;


    private bool IsDied;
    private bool isCharge;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        monster = GetComponent<Skeleton>();
        playerTr = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<MonsterStat>();
        isCharge = false;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        monsterTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        IsDied = false;
        agent.speed = stat.GetMoveSpeed();
        agent.stoppingDistance = 0.1f;
    }

    private void Update()
    {
        // agent.ResetPath();
        if (!stat.IsAlive())
        {
            gameObject.SetActive(false); //수정
            GetComponentInParent<MonsterManager>().RemoveMonster(monster); //수정
            StopAllCoroutines();
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(playerTr.position, transform.position) <= stat.GetAttackDistance() && isCharge == false) //5f
        {
            StartCoroutine(Charge());
        }

    }

    private IEnumerator Charge()
    {
        isCharge = true;
        while (true)
        {
            agent.ResetPath();
            Vector3 position = playerTr.position;
            agent.SetDestination(position);
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0.5f, 3f));
        }

    }


    private void LateUpdate()
    {
        if (playerTr == null)
        {
            return;
        }
        if (playerTr.position.x >= this.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

}
