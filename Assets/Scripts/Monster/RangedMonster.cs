using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.AI;
public enum MONSTER_RANGED
{
    NORMAL,
    BOSS
}

public class RangedMonster : Monster
{
    
    public void Awake() 
    {
        CurrentHP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;
        circleCollider = GetComponent<CircleCollider2D>();
        monsterController = GetComponent<MonsterController>();
    }

    public void Start() 
    {
        attackDistance = 2f;
        detectionDistance = 4f;
    }

    public override void Attack() 
    {
        Debug.Log("ranger attack");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) 
            return;

        //CurrentHP -= collision.GetComponent<Bullet>().damage; 
        Debug.Log("shooting monster");
    }
}