using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssembleController : MonoBehaviour {
    private Assemble monster;
    
    private Transform playerTr;
    private Transform monsterTr;

    private NavMeshAgent agent;
    private MonsterStat stat;
    private MonsterShooter monsterShooter;
    private bool IsDied;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        monster = GetComponent<Assemble>();
        playerTr = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<MonsterStat>();
        monsterShooter = GetComponent<MonsterShooter>();

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
        agent.ResetPath();
        if (!stat.IsAlive())
        {
            gameObject.SetActive(false); //수정
            GetComponentInParent<MonsterManager>().RemoveMonster(monster); //수정
            Destroy(gameObject);
            return;
        }
        if (Vector3.Distance(playerTr.position, transform.position) <= stat.GetAttackDistance()) //5f
        {
            monsterShooter.Shoot();
            agent.SetDestination(playerTr.position);
        } 
        else if (Vector3.Distance(playerTr.position, transform.position) <= stat.GetDetectionDistance()) //8f
        {
            agent.SetDestination(playerTr.position);
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

