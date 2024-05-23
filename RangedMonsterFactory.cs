using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterFactory : MonsterFactory<MONSTER_RANGED>
{
    [SerializeField]
    private GameObject normalRangedPrefab;
    [SerializeField]
    private GameObject bossRangedPrefab;
    
    protected override Monster Create(MONSTER_RANGED _type, Dictionary<int, Stat> Monsterdict)
    {
        RangedMonster rangedMonster = null;
        switch (_type)
        {
            case MONSTER_RANGED.NORMAL :
                rangedMonster = Instantiate(this.normalRangedPrefab).GetComponent<RangedMonster>();
                break;
            case MONSTER_RANGED.BOSS :
                rangedMonster = Instantiate(this.bossRangedPrefab).GetComponent<RangedMonster>();
                break;
        }
        //json데이터 가져와서 프리펩 정보 수정하기
        int ID = rangedMonster.GetComponent<MonsterStat>().ID;
        rangedMonster.GetComponent<MonsterStat>().SetMonsterName(Monsterdict[ID].MonsterName);
        rangedMonster.GetComponent<MonsterStat>().SetDesc(Monsterdict[ID].Desc);
        rangedMonster.GetComponent<MonsterStat>().SetAttackDistance(Monsterdict[ID].AttackDistance);
        rangedMonster.GetComponent<MonsterStat>().SetDetectionDistance(Monsterdict[ID].DetectionDistance);
        rangedMonster.GetComponent<MonsterStat>().SetMaxHP(Monsterdict[ID].fMaxHP);
        rangedMonster.GetComponent<MonsterStat>().SetCurrentHP(Monsterdict[ID].fCurrentHP);
        rangedMonster.GetComponent<MonsterStat>().SetDamage(Monsterdict[ID].fDamage);

        rangedMonster.gameObject.SetActive(true);
        rangedMonster.gameObject.tag = "RangedMonster";
        return rangedMonster;
    }
}
