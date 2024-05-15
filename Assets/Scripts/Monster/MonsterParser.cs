using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MonsterParser: MonoBehaviour
{
    public static MonsterParser mInstance;
    public Transform parent;
    public List<Monster> monsters; // 몬스터 목록

    private void Awake()
    {
        if (mInstance != null)
        {
            Debug.Assert(false, "MonsterParser Class is Singleton.");
        }
        else
        {
            this.gameObject.tag = "Parser";
            DontDestroyOnLoad(this.gameObject);
            mInstance = this;
           // GameObject.FindGameObjectWithTag("Parser").transform.Find("Monster Parser");
            StartCoroutine("LoadCoroutine");
        }
    }

    IEnumerator LoadCoroutine()
    {
        string monsterJsonString = File.ReadAllText(Application.dataPath + "/Resources/Data/MonsterData.json");

        // JSON 파싱
        MonsterData monsterData = JsonUtility.FromJson<MonsterData>(monsterJsonString);

        // 몬스터 목록 초기화
        monsters = new List<Monster>();

        // foreach (var data in monsterData.monsterData)
        // {
        //     Monster _monster = new Monster();
        //     _monster.ID = int.Parse(data.ID);
        //     _monster.Type = (MonsterType)int.Parse(data.Type);
        //     _monster.MaxHP = int.Parse(data.MaxHP);
        //     _monster.Speed = int.Parse(data.Speed);
        //     _monster.Power = int.Parse(data.Power);
        //     // _monster.MonsterStatus.CurrentHP = _monster.MaxHP;

        //     monsters.Add(_monster);

        //     GameObject monsterPrefab = ToPrefab(data.MonsterPrefabPath);
        //     GameObject monsterInstance = Instantiate(monsterPrefab); // Instantiate prefab
        //     monsterInstance.AddComponent<MonsterAdapter>();
        //     monsterInstance.GetComponent<MonsterAdapter>().monster = _monster;
        //     monsterInstance.AddComponent<Status>();
        //     monsterInstance.GetComponent<Status>().CurrentHP = _monster.MaxHP;
        //     monsterInstance.name = "Monster" + data.ID;
        //     monsterInstance.tag = "Monster";
        //     monsterInstance.transform.SetParent(this.transform);
        // }

        foreach (var data in monsterData.monsterData)
        {
            // 프리팹에서 직접 생성
            GameObject monsterPrefab = ToPrefab(data.MonsterPrefabPath);
            if (monsterPrefab != null)
            {
                GameObject monsterInstance = Instantiate(monsterPrefab, parent);
                monsterInstance.name = "Monster" + data.ID;
                monsterInstance.tag = "Monster";
                MonsterController monsterController = monsterInstance.GetComponent<MonsterController>();
                Debug.Log(monsterController);
                
                if (monsterController != null)
                {
                    // 데이터 설정
                    monsterController.GetComponent<MonsterAdapter>().monster.ID = int.Parse(data.ID);
                    monsterController.GetComponent<MonsterAdapter>().monster.Type = (MonsterType)int.Parse(data.Type);
                    monsterController.GetComponent<MonsterAdapter>().monster.MaxHP = int.Parse(data.MaxHP);
                    monsterController.GetComponent<Status>().CurrentHP = int.Parse(data.MaxHP);
                    monsterController.GetComponent<MonsterAdapter>().monster.Speed = int.Parse(data.Speed);
                    monsterController.GetComponent<MonsterAdapter>().monster.Power = int.Parse(data.Power); 
                }
                else
                {
                    Debug.LogError("Monster prefab does not have MonsterController component: " + data.MonsterPrefabPath);
                }
            }
            else
            {
                Debug.LogError("Failed to load monster prefab at path: " + data.MonsterPrefabPath);
            }
        }

        // 몬스터 정보 출력 
        foreach (var monster in monsters)
        {
            Debug.Log("Type: " + monster.Type + ", MaxHP: " + monster.MaxHP + ", Speed: " + monster.Speed + 
            ", Power: " + monster.Power);
        }

        yield return null;
    }

    public Monster getMonsterByID(int id)
    {
        if (monsters.Count>id) return monsters[id];
        return null;
    }

    public GameObject ToPrefab(String monsterPrefabPath) 
    {
        GameObject monsterPrefab = Resources.Load(monsterPrefabPath) as GameObject;
        return monsterPrefab;
    }

    public void GenerateMonsterPool(int _ID, SpawnPoint _SpawnPoint)
    {
        // GameObject obj = GameObject.FindGameObjectWithTag("Monster").transform.Find("Monster" + _ID).gameObject;
        Transform obj = GameObject.FindGameObjectWithTag("Parser").transform.Find("Monster Parser").Find("Monster" + _ID);
        Transform tr = Instantiate(obj, Vector3.zero, Quaternion.identity);
        tr.gameObject.SetActive(true);
        tr.gameObject.name = _ID +"";
        tr.gameObject.GetComponent<MonsterController>().spawnPoint = _SpawnPoint;
        tr.parent = GameObject.FindGameObjectWithTag("Pool").transform.Find("MonsterPool").Find(_SpawnPoint.gameObject.name);

        // GameObject clone = Instantiate(obj, Vector3.zero, Quaternion.identity);
        // clone.name = "Monster" + _ID; 
        // clone.GetComponent<MonsterController>().spawnPoint = _SpawnPoint;
        // clone.SetActive(true);
        // clone.transform.SetParent(GameObject.FindGameObjectWithTag("Pool").transform.Find("MonsterPool")); // 올바른 부모로 설정
    }
}