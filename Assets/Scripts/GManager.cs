using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    private int iKills;  //ų��
    private int iKillsToGetItem;  //������ ��� ���� �����ؾ� �ϴ� ų ��
    private int iItemPoolSize;  //������ Ǯ ������

    private Vector3 ItemPosition;

    private Dictionary<TestItem.ItemType, Queue<GameObject>> itemPools;

    void Start()
    {
        iKillsToGetItem = 3;
        iKills = 0;
        iItemPoolSize = 4; // ���ϴ� Ǯ ũ�� ����
        ItemPosition = new Vector3(0, 0,0);

        itemPools = new Dictionary<TestItem.ItemType, Queue<GameObject>>()
        {
            { TestItem.ItemType.normal, new Queue<GameObject>() },
            { TestItem.ItemType.rare, new Queue<GameObject>() },
            { TestItem.ItemType.unique, new Queue<GameObject>() }
        };

        // Resources ���� �� ��� ������ �ε�
        GameObject[] allItems = Resources.LoadAll<GameObject>("Prefabs/Items");

        // �ε�� ������ �߿��� �������� �����Ͽ� Ǯ�� �ʱ�ȭ
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
                TestItem testItem = itemToPool.GetComponent<TestItem>();
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
        

        // 60% Ȯ���� normal, 30% Ȯ���� rare, 10% Ȯ���� unique ������ ����
        float randomValue = Random.Range(0f, 1f);
        TestItem.ItemType selectedType;

        if (randomValue < 0.6f)
        {
            selectedType = TestItem.ItemType.normal;
            Debug.Log("normal");
        }
        else if (randomValue < 0.9f)
        {
            selectedType = TestItem.ItemType.rare;
            Debug.Log("rare");
        }
        else
        {
            selectedType = TestItem.ItemType.unique;
            Debug.Log("unique");
        }

        Queue<GameObject> selectedPool = itemPools[selectedType];

        if (selectedPool.Count > 0)
        {
            iKills = 0; // ų �� �ʱ�ȭ
            GameObject item = selectedPool.Dequeue();
            item.transform.position=ItemPosition;
            item.SetActive(true);
           
            iKillsToGetItem+=3; //������ ��� ���� ų�� ����: ���̵� ��������
        }
    }

    public void ReturnItemToPool(GameObject item, TestItem.ItemType type)
    {
        item.SetActive(false);
        itemPools[type].Enqueue(item);
    }
}
