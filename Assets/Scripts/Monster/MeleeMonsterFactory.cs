using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterFactory : MonsterFactory<MONSTER_MELEE>
{
    [SerializeField]
    private GameObject normalMeleePrefab; //add prefab
    [SerializeField]
    private GameObject bossMeleePrefab; //add prefab
    
    protected override Monster Create(MONSTER_MELEE _type)
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
        meleeMonster.gameObject.SetActive(true);
        meleeMonster.gameObject.tag = "MeleeMonster";
        return meleeMonster;
    }
}
