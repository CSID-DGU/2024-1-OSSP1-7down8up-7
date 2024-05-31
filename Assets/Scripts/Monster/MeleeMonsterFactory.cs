using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterFactory : MonsterFactory<MONSTER_MELEE>
{
    [SerializeField]
    private List<GameObject> normalMeleePrefabs; //add prefab
    [SerializeField]
    private List<GameObject> bossMeleePrefabs; //add prefab
    
    protected override Monster Create(MONSTER_MELEE _type, Dictionary<int, Stat> Monsterdict)
    {
        MeleeMonster meleeMonster = null;
        int idx = 0;
        switch (_type)
        {
            case MONSTER_MELEE.NORMAL:
                idx = Random.Range(0, normalMeleePrefabs.Count);
                meleeMonster = Instantiate(this.normalMeleePrefabs[idx]).GetComponent<MeleeMonster>();
                
                break;
            case MONSTER_MELEE.BOSS:
                idx = Random.Range(0, bossMeleePrefabs.Count);
                meleeMonster = Instantiate(this.bossMeleePrefabs[idx]).GetComponent<MeleeMonster>();

                break;

        }
        //json������ �����ͼ� ������ ���� �����ϱ�
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
