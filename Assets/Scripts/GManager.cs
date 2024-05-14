using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    private int iKills;
    private int iKillsToGetItem;
    private int iItemPoolSize;

    public Vector2 ItemPosition;


    void Start()
    {
        iKillsToGetItem = 3;
        iKills = 0;
        iItemPoolSize = 3; // 원하는 풀 크기 설정
        ItemPosition=new Vector2(0,0);

        // Resources 폴더 내 모든 프리팹 로드
        GameObject[] allItems = Resources.LoadAll<GameObject>("Prefabs/Items");

        // 로드된 프리팹 중에서 랜덤으로 선택하여 풀을 초기화
        InitializePoolRandomly(allItems, iItemPoolSize);
    }

    void InitializePoolRandomly(GameObject[] allItems, int poolSize)
    {
        ObjectPool.SharedInstance.pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            if (allItems.Length > 0)
            {
                GameObject itemToPool = allItems[Random.Range(0, allItems.Length)];
                GameObject item = Instantiate(itemToPool);
                item.SetActive(false);
                ObjectPool.SharedInstance.pooledObjects.Add(item);
                Debug.Log(item.name);
            }
        }
    }

    void Update()
    {
    }

    public void KillsPlusOne()
    {
        Debug.Log("킬수 +1");
        iKills += 1;
    }

    public bool isEqualKills()
    {
        if (iKills == iKillsToGetItem)
            return true;
        else
           return false;
    }

    public void SpawnItem()
    {
        Debug.Log("킬수 달성");
        GameObject item = ObjectPool.SharedInstance.GetPooledObject();
        if (item != null)
        {
            item.SetActive(true);
            iKills = 0; // 킬 수 초기화
            iKillsToGetItem = iKillsToGetItem + 3;
        }
    }
}
