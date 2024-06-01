using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMonsterFactory : MonsterFactory<MONSTER_BOSS>
{
    [SerializeField]
    private GameObject SnakePrefab; 
    
    protected override Monster Create(MONSTER_BOSS _type, Dictionary<int, Stat> Monsterdict)
    {
        BossMonster bossMonster = null;

        switch (_type)
        {
            case MONSTER_BOSS.SNAKE:
                bossMonster = Instantiate(SnakePrefab).GetComponent<BossMonster>();
                
                break;
        }
        //json������ �����ͼ� ������ ���� �����ϱ�
        int ID = bossMonster.GetComponent<MonsterStat>().ID;
        bossMonster.GetComponent<MonsterStat>().SetMonsterName(Monsterdict[ID].MonsterName);
        bossMonster.GetComponent<MonsterStat>().SetDesc(Monsterdict[ID].Desc);
        bossMonster.GetComponent<MonsterStat>().SetAttackDistance(Monsterdict[ID].AttackDistance);
        bossMonster.GetComponent<MonsterStat>().SetDetectionDistance(Monsterdict[ID].DetectionDistance);
        bossMonster.GetComponent<MonsterStat>().SetMaxHP(Monsterdict[ID].fMaxHP);
        bossMonster.GetComponent<MonsterStat>().SetCurrentHP(Monsterdict[ID].fCurrentHP);
        bossMonster.GetComponent<MonsterStat>().SetDamage(Monsterdict[ID].fDamage);

        bossMonster.GetComponent<MonsterStat>().SetMoveSpeed(Monsterdict[ID].fMoveSpeed);
        bossMonster.GetComponent<MonsterStat>().SetBulletSpeed(Monsterdict[ID].fBulletSpeed);
        bossMonster.GetComponent<MonsterStat>().SetBulletLifeTime(Monsterdict[ID].fBulletLifeTime);
        bossMonster.GetComponent<MonsterStat>().SetTimeBetweenShots(Monsterdict[ID].timeBetweenShots);

        bossMonster.gameObject.SetActive(true);
        bossMonster.gameObject.tag = "BossMonster";
        return bossMonster;
    }
}
