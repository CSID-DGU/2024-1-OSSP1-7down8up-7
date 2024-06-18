using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    public GameObject BossMonsterFactoryPrefab;
    private BossMonsterFactory bossMonsterFactory;

    public List<Monster> monsters = new List<Monster>();
    DataManager _data = new DataManager();
    Dictionary<int, Stat> Monsterdict;


    private RectTransform rectTransform;

    private RoomInstance roomInstance;
    private void Awake()
    {
        _data.Init();
        Monsterdict = _data.MonsterStatDict;

        meleeMonsterFactory = MeleeMonsterFactoryPrefab.GetComponent<MeleeMonsterFactory>();
        rangedMonsterFactory = RangedMonsterFactoryPrefab.GetComponent<RangedMonsterFactory>();
        debuffMonsterFactory = DebuffMonsterFactoryPrefab.GetComponent<DebuffMonsterFactory>();
        bossMonsterFactory = BossMonsterFactoryPrefab.GetComponent<BossMonsterFactory>();

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
    public void SpawnBossMonster(int bossMonsterType)
    {
        Vector3 spawnPosition = GetRandomPositionInRoom();
        monsters.Add(bossMonsterFactory.Spawn((MONSTER_BOSS)bossMonsterType, spawnPosition, this.transform, Monsterdict));
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
        if (roomInstance.type == 0)
        {
            int meleeNum = Random.Range(1, 3);
            int rangeNum = Random.Range(0, 3);
            int debuffNum = Random.Range(0, 2);
            
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
        }
        else if (roomInstance.type == 2)
        {
            int curStage = GameObject.FindWithTag("Player").GetComponent<PlayerStat>().GetPlayerStage();
            if (curStage == 1)
            {
                SpawnBossMonster(0); //snake 호출
            }
            else if (curStage == 2)
            {
                SpawnBossMonster(1); //Assemble호출
            }
            else if (curStage == 3)
            {
                SpawnBossMonster(2); //Skeleton1호출
                SpawnBossMonster(3); //Skeleton2호출
            }
        }
    }

    public void Update()
    {
        if (roomInstance.type != 1 && monsters.Count == 0)
        {
            GameManager game=GameObject.Find("GameManager").GetComponent<GameManager>();
            game._data.SaveData();
            float randomValue = Random.Range(0f, 1f);
            if (randomValue < 0.3f)
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

    public int getMonsterCount()
    {
        return monsters.Count;
    }
}