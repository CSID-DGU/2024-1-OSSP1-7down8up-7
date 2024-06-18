using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterFactory : MonsterFactory<MONSTER_RANGED>
{
    [SerializeField]
    private List<GameObject> rangedPrefabs;
    
    protected override Monster Create(MONSTER_RANGED _type, Dictionary<int, Stat> Monsterdict)
    {
        RangedMonster rangedMonster = null;
        int idx = Random.Range(0, rangedPrefabs.Count);
        rangedMonster = Instantiate(rangedPrefabs[idx]).GetComponent<RangedMonster>();
        
        //json������ �����ͼ� ������ ���� �����ϱ�
        int ID = rangedMonster.GetComponent<MonsterStat>().ID;
        rangedMonster.GetComponent<MonsterStat>().SetMonsterName(Monsterdict[ID].MonsterName);
        rangedMonster.GetComponent<MonsterStat>().SetDesc(Monsterdict[ID].Desc);
        rangedMonster.GetComponent<MonsterStat>().SetAttackDistance(Monsterdict[ID].AttackDistance);
        rangedMonster.GetComponent<MonsterStat>().SetDetectionDistance(Monsterdict[ID].DetectionDistance);
        rangedMonster.GetComponent<MonsterStat>().SetMaxHP(Monsterdict[ID].fMaxHP);
        rangedMonster.GetComponent<MonsterStat>().SetCurrentHP(Monsterdict[ID].fCurrentHP);
        rangedMonster.GetComponent<MonsterStat>().SetDamage(Monsterdict[ID].fDamage);

        rangedMonster.GetComponent<MonsterStat>().SetMoveSpeed(Monsterdict[ID].fMoveSpeed);
        rangedMonster.GetComponent<MonsterStat>().SetBulletSpeed(Monsterdict[ID].fBulletSpeed);
        rangedMonster.GetComponent<MonsterStat>().SetBulletLifeTime(Monsterdict[ID].fBulletLifeTime);
        rangedMonster.GetComponent<MonsterStat>().SetTimeBetweenShots(Monsterdict[ID].timeBetweenShots);

        rangedMonster.gameObject.SetActive(true);
        rangedMonster.gameObject.tag = "RangedMonster";
        return rangedMonster;
    }
}
