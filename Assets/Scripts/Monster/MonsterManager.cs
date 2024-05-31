using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//room이 생성될 때 같이 생성되어 room 오브젝트의 하위 오브젝트로 들어간다.
//후에 stage1MonsterManager, stage2MonsterManager로 확장해야한다. 
public class MonsterManager : MonoBehaviour
{
    public GameObject MeleeMonsterFactoryPrefab;
    private MeleeMonsterFactory meleeMonsterFactory;
    public GameObject RangedMonsterFactoryPrefab;
    private RangedMonsterFactory rangedMonsterFactory;
    public GameObject DebuffMonsterFactoryPrefab;
    private DebuffMonsterFactory debuffMonsterFactory;

    public List<Monster> monsters = new List<Monster>();
    DataManager _data = new DataManager();
    Dictionary<int, Stat> Monsterdict;

    private bool isMeleeBossSpawned = false;
    private bool isRangedBossSpawned = false;
    private bool isDebuffBossSpawned = false;

    private RectTransform rectTransform;

    private RoomInstance roomInstance;
    private void Awake()
    {
        _data.Init();
        Monsterdict = _data.MonsterStatDict;

        meleeMonsterFactory = MeleeMonsterFactoryPrefab.GetComponent<MeleeMonsterFactory>();
        rangedMonsterFactory = RangedMonsterFactoryPrefab.GetComponent<RangedMonsterFactory>();
        debuffMonsterFactory = DebuffMonsterFactoryPrefab.GetComponent<DebuffMonsterFactory>();

        rectTransform = GetComponentInParent<RectTransform>();
        roomInstance = GetComponentInParent<RoomInstance>();
    }

    public void SpawnMeleeMonster(int meleeMonsterType)
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();
        monsters.Add(meleeMonsterFactory.Spawn((MONSTER_MELEE)meleeMonsterType, spawnPosition, this.transform, Monsterdict));
    }
    public void SpawnRangedMonster(int rangedMonsterType)
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();
        monsters.Add(rangedMonsterFactory.Spawn((MONSTER_RANGED)rangedMonsterType, spawnPosition, this.transform, Monsterdict));
    }
    public void SpawnDebuffMonster(int debuffMonsterType)
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();
        monsters.Add(debuffMonsterFactory.Spawn((MONSTER_DEBUFF)debuffMonsterType, spawnPosition, this.transform, Monsterdict));
    }

    public Vector3 GetRandomPositionInRoom()
    {
        float randomX = Random.Range((float)rectTransform.rect.xMin + 1.0f, (float)rectTransform.rect.xMax - 1.0f);
        float randomY = Random.Range((float)rectTransform.rect.yMin + 1.0f, (float)rectTransform.rect.yMax - 1.0f);

        Vector3 randomPos = new Vector3(randomX, randomY, 0);
        return randomPos;
    }

    public void Start()
    {


        //normal monster call
        init();
    }
    public void init()
    {
         int meleeNum = Random.Range(2, 5);
         int rangeNum = Random.Range(2, 5);
         int debuffNum = Random.Range(1, 4);

         for (int i = 0; i < meleeNum; i++)
         {
             SpawnMeleeMonster(0);
         }
         
          for (int i = 0; i < rangeNum; i++)
         {
             SpawnRangedMonster(0);
         }

         for (int i = 0; i < debuffNum; i++)
         {
             SpawnDebuffMonster(0);
         }

        // if (roomInstance.type == 2)
        // {
        //     SpawnMeleeMonster(1);
        // }
        // SpawnMeleeMonster(1);
    }

    public void Update()
    {
        if (monsters.Count == 0)
        {
            GameManager game=GameObject.Find("GameManager").GetComponent<GameManager>();
            game._data.SaveData();
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < 0.99f)
            {
                Vector3 position = this.transform.position;
                ItemManager Items = GameObject.Find("ItemManager").GetComponent<ItemManager>();
                Items.SetItemPosition(position);
                Items.SpawnItem();
            }
            
            gameObject.SetActive(false);
        }
    }

    public void RemoveMonster(Monster monster)
    {

        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
        }
    }
}