using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//room이 생성될 떄 같이 생성되어 room 오브젝트의 하위 오브젝트로 들어간다.
//후에 stage1MonsterManager, stage2MonsterManager로 확장해야한다. 
public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    private GameObject map; //나중에 room(map) 클래스로 바꿔야됨
    [SerializeField]
    private MeleeMonsterFactory meleeMonsterFactory;
    [SerializeField]
    private RangedMonsterFactory rangedMonsterFactory;

    public List<Monster> monsters = new List<Monster>();

    private void Awake()
    {
    }

    public void SpawnMeleeMonster(int meleeMonsterType)
    {
        monsters.Add(this.meleeMonsterFactory.Spawn((MONSTER_MELEE)meleeMonsterType, map));
    }
    public void SpawnRangedMonster(int rangedMonsterType)
    {
        monsters.Add(this.rangedMonsterFactory.Spawn((MONSTER_RANGED)rangedMonsterType, map));
    }

    public void Start() 
    {
        int meleeNum = Random.Range(1,10);
        int rangeNum = Random.Range(1,10);

        for (int i=0;i<meleeNum;i++)
        {
            SpawnMeleeMonster(0);
        }

        for (int i=0;i<rangeNum;i++)
        {
            SpawnRangedMonster(0);
        }
    }


}
