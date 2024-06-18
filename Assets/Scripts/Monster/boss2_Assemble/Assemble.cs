using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static PassiveItem;

public class Assemble : BossMonster
{
    private MonsterShooter monsterShooter;
    private BoxCollider2D boxCollider2D;
    
    public void Start()
    {
        monsterShooter = GetComponent<MonsterShooter>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void Attack() 
    {
        monsterShooter.Shoot();
    }
}