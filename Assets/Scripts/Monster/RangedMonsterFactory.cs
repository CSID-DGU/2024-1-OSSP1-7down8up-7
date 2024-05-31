using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterFactory : MonsterFactory<MONSTER_RANGED>
{
    [SerializeField]
    private List<GameObject> normalRangedPrefabs;
    [SerializeField]
    private List<GameObject> bossRangedPrefabs;
    
    protected override Monster Create(MONSTER_RANGED _type, Dictionary<int, Stat> Monsterdict)
    {
        RangedMonster rangedMonster = null;
        int idx = 0;
        switch (_type)
        {
            case MONSTER_RANGED.NORMAL :
                idx = Random.Range(0, normalRangedPrefabs.Count);
                rangedMonster = Instantiate(this.normalRangedPrefabs[idx]).GetComponent<RangedMonster>();
                break;
            case MONSTER_RANGED.BOSS :
                idx = Random.Range(0, bossRangedPrefabs.Count);
                rangedMonster = Instantiate(this.bossRangedPrefabs[idx]).GetComponent<RangedMonster>();
                break;
        }
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
