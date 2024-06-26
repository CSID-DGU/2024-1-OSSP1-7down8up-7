using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterType
{
    Melee = 0,
    Range = 1,
    Debuff = 2,
}

public abstract class Monster :MonoBehaviour
{
    public int ID;
    public string monsterName;
    public string desc;
    public float MaxHP;
    public float CurrentHP;
    public float Power;

    public NavMeshAgent agent;
    public Rigidbody2D rb2D;
    public CircleCollider2D circleCollider;
    public MonsterController monsterController;

    public float attackDistance;
    public float detectionDistance;
    public bool IsAttacking;
    public Animator animator;

    
    public abstract void Attack();
}

