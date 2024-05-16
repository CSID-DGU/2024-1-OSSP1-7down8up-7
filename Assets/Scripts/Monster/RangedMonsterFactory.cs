using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterFactory : MonsterFactory<MONSTER_RANGED>
{
    [SerializeField]
    private GameObject normalRangedPrefab;
    [SerializeField]
    private GameObject bossRangedPrefab;
    
    protected override Monster Create(MONSTER_RANGED _type)
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
        rangedMonster.gameObject.SetActive(true);
        rangedMonster.gameObject.tag = "RangedMonster";
        return rangedMonster;
    }
}
