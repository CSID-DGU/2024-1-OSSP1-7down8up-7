using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    private int iKills;  //킬수
    private int iKillsToGetItem;  //아이템 얻기 위해 충족해야 하는 킬 수
    private int iItemPoolSize;  //아이템 풀 사이즈

    private Vector3 ItemPosition;

    private Dictionary<PassiveItem.Rarity, Queue<GameObject>> itemPools;

    void Start()
    {
        iKillsToGetItem = 3;
        iKills = 0;
        iItemPoolSize = 4; // 원하는 풀 크기 설정
        ItemPosition = new Vector3(0, 0,0);

        itemPools = new Dictionary<PassiveItem.Rarity, Queue<GameObject>>()
        {
            { PassiveItem.Rarity.normal, new Queue<GameObject>() },
            { PassiveItem.Rarity.rare, new Queue<GameObject>() },
            { PassiveItem.Rarity    .unique, new Queue<GameObject>() }
        };

        // Resources 폴더 내 모든 프리팹 로드
        GameObject[] allItems = Resources.LoadAll<GameObject>("Prefabs/Items");

        // 로드된 프리팹 중에서 랜덤으로 선택하여 풀을 초기화
        InitializePoolRandomly(allItems, iItemPoolSize);
    }

    void Update()
    {
    }

    void InitializePoolRandomly(GameObject[] allItems, int poolSize)
    {
        foreach (var pool in itemPools.Values)
        {
            pool.Clear();
        }

        for (int i = 0; i < poolSize; i++)
        {
            if (allItems.Length > 0)
            {
                GameObject itemToPool = allItems[Random.Range(0, allItems.Length)];
                PassiveItem testItem = itemToPool.GetComponent<PassiveItem>();
                if (testItem != null)
                {
                    GameObject item = Instantiate(itemToPool);
                   
                    item.SetActive(false);
                    itemPools[testItem.type].Enqueue(item);
                    Debug.Log(item.name);
                }
            }
        }
    }

  

    public void KillsPlusOne()
    {
        iKills += 1;
    }
    public bool isEqualKills()
    {
        return iKills == iKillsToGetItem;
    }
    
    
    public void SetItemPosition(Vector3 position)
    {
        ItemPosition=position;

    }
    public Vector3 GetItemPosition()
    {
        return ItemPosition;
    }
   
    public void SpawnItem()
    {
        

        // 60% 확률로 normal, 30% 확률로 rare, 10% 확률로 unique 아이템 생성
        float randomValue = Random.Range(0f, 1f);
        PassiveItem.Rarity selectedType;

        if (randomValue < 0.6f)
        {
            selectedType = PassiveItem.Rarity.normal;
        }
        else if (randomValue < 0.9f)
        {
            selectedType = PassiveItem.Rarity.rare;
        }
        else
        {
            selectedType = PassiveItem.Rarity.unique;      
        }

        Queue<GameObject> selectedPool = itemPools[selectedType];

        if (selectedPool.Count > 0)
        {
            iKills = 0; // 킬 수 초기화
            GameObject item = selectedPool.Dequeue();
            item.transform.position=ItemPosition;
            item.SetActive(true);
           
            iKillsToGetItem+=3; //아이템 얻기 위한 킬수 증가: 난이도 조절용임
        }
    }

    public void ReturnItemToPool(GameObject item, PassiveItem.Rarity type)
    {
        item.SetActive(false);  
        itemPools[type].Enqueue(item);
    }
}
