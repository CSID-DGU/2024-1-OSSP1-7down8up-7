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
        iItemPoolSize = 3; // ���ϴ� Ǯ ũ�� ����
        ItemPosition=new Vector2(0,0);

        // Resources ���� �� ��� ������ �ε�
        GameObject[] allItems = Resources.LoadAll<GameObject>("Prefabs/Items");

        // �ε�� ������ �߿��� �������� �����Ͽ� Ǯ�� �ʱ�ȭ
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
        Debug.Log("ų�� +1");
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
        Debug.Log("ų�� �޼�");
        GameObject item = ObjectPool.SharedInstance.GetPooledObject();
        if (item != null)
        {
            item.SetActive(true);
            iKills = 0; // ų �� �ʱ�ȭ
            iKillsToGetItem = iKillsToGetItem + 3;
        }
    }
}
