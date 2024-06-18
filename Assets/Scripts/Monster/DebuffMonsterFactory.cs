using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffMonsterFactory : MonsterFactory<MONSTER_DEBUFF>
{
    [SerializeField]
    private List<GameObject> debuffPrefabs; //add prefab
    protected override Monster Create(MONSTER_DEBUFF _type, Dictionary<int, Stat> Monsterdict)
    {
        DebuffMonster debuffMonster = null;
        int idx = Random.Range(0, debuffPrefabs.Count);
        debuffMonster = Instantiate(debuffPrefabs[idx]).GetComponent<DebuffMonster>();

        //json������ �����ͼ� ������ ���� �����ϱ�
        int ID = debuffMonster.GetComponent<MonsterStat>().ID;
        debuffMonster.GetComponent<MonsterStat>().SetMonsterName(Monsterdict[ID].MonsterName);
        debuffMonster.GetComponent<MonsterStat>().SetDesc(Monsterdict[ID].Desc);
        debuffMonster.GetComponent<MonsterStat>().SetAttackDistance(Monsterdict[ID].AttackDistance);
        debuffMonster.GetComponent<MonsterStat>().SetDetectionDistance(Monsterdict[ID].DetectionDistance);
        debuffMonster.GetComponent<MonsterStat>().SetMaxHP(Monsterdict[ID].fMaxHP);
        debuffMonster.GetComponent<MonsterStat>().SetCurrentHP(Monsterdict[ID].fCurrentHP);
        debuffMonster.GetComponent<MonsterStat>().SetDamage(Monsterdict[ID].fDamage);

        debuffMonster.GetComponent<MonsterStat>().SetMoveSpeed(Monsterdict[ID].fMoveSpeed);
        debuffMonster.GetComponent<MonsterStat>().SetBulletSpeed(Monsterdict[ID].fBulletSpeed);
        debuffMonster.GetComponent<MonsterStat>().SetBulletLifeTime(Monsterdict[ID].fBulletLifeTime);
        debuffMonster.GetComponent<MonsterStat>().SetTimeBetweenShots(Monsterdict[ID].timeBetweenShots);

        debuffMonster.gameObject.SetActive(true);
        debuffMonster.gameObject.tag = "DebuffMonster";
        return debuffMonster;
    }
}
