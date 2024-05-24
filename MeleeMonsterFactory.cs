using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MeleeMonsterFactory : MonsterFactory<MONSTER_MELEE>
{
    [SerializeField]
    private GameObject normalMeleePrefab; //add prefab
    [SerializeField]
    private GameObject bossMeleePrefab; //add prefab
    
    protected override Monster Create(MONSTER_MELEE _type, Dictionary<int, Stat> Monsterdict)
    {
        MeleeMonster meleeMonster = null;
        switch (_type)
        {
            case MONSTER_MELEE.NORMAL:
                meleeMonster = Instantiate(this.normalMeleePrefab).GetComponent<MeleeMonster>();
                
                break;
            case MONSTER_MELEE.BOSS:
                meleeMonster = Instantiate(this.bossMeleePrefab).GetComponent<MeleeMonster>();

                break;

        }
        //json데이터 가져와서 프리펩 정보 수정하기
        int ID = meleeMonster.GetComponent<MonsterStat>().ID;
        meleeMonster.GetComponent<MonsterStat>().SetMonsterName(Monsterdict[ID].MonsterName);
        meleeMonster.GetComponent<MonsterStat>().SetDesc(Monsterdict[ID].Desc);
        meleeMonster.GetComponent<MonsterStat>().SetAttackDistance(Monsterdict[ID].AttackDistance);
        meleeMonster.GetComponent<MonsterStat>().SetDetectionDistance(Monsterdict[ID].DetectionDistance);
        meleeMonster.GetComponent<MonsterStat>().SetMaxHP(Monsterdict[ID].fMaxHP);
        meleeMonster.GetComponent<MonsterStat>().SetCurrentHP(Monsterdict[ID].fCurrentHP);
        meleeMonster.GetComponent<MonsterStat>().SetDamage(Monsterdict[ID].fDamage);

        meleeMonster.GetComponent<MonsterStat>().SetMoveSpeed(Monsterdict[ID].fMoveSpeed);
        meleeMonster.GetComponent<MonsterStat>().SetBulletSpeed(Monsterdict[ID].fBulletSpeed);
        meleeMonster.GetComponent<MonsterStat>().SetBulletLifeTime(Monsterdict[ID].fBulletLifeTime);
        meleeMonster.GetComponent<MonsterStat>().SetTimeBetweenShots(Monsterdict[ID].timeBetweenShots);

        meleeMonster.gameObject.SetActive(true);
        meleeMonster.gameObject.tag = "MeleeMonster";
        return meleeMonster;
    }
}
