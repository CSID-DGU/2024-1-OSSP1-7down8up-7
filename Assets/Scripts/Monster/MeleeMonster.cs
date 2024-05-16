using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MONSTER_MELEE
{
    NORMAL,
    BOSS
}

public class MeleeMonster : Monster
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

    public void OnEnable() 
    {
        attackDistance = 0.1f;
        detectionDistance = 7.0f;
    }

    public override void Attack() 
    {
        GameManager.instance.health -= this.Power; //decrease player's health
        Debug.Log("Player's currentHP = "+GameManager.instance.health);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) 
            return;

        //CurrentHP -= collision.GetComponent<Bullet>().damage; 
        Debug.Log("shooting monster");
    }
}