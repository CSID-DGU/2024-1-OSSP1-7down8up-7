using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeHeadController : SnakePartController
{
    private Snake monster;
    
    public List<SnakeBodyController> bodyParts = new List<SnakeBodyController>();
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
        monster = GetComponent<Snake>();
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
            DestroyAllBodyParts();
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

    public void RegisterBodyPart(SnakeBodyController bodyPart)
    {
        bodyParts.Add(bodyPart);
    }

    private void DestroyAllBodyParts()
    {
        foreach (var bodyPart in bodyParts)
        {
            if (bodyPart != null)
            {
                Destroy(bodyPart.gameObject);
            }
        }
    }
}
