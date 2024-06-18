using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    private DataManager _data;
    private int iItemPoolSize;  // ������ Ǯ ������
    private Vector3 ItemPosition;
    private Dictionary<PassiveItem.Rarity, Queue<GameObject>> itemPools;
    GameObject[] allItems;


    private void Awake()
    {
        // Singleton ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Init()
    {
        _data = new DataManager();
        _data.Init();
        Dictionary<float, Item> Itemdict = _data.ItemDict;

        ItemPosition = new Vector3(0, 0, 0);

        itemPools = new Dictionary<PassiveItem.Rarity, Queue<GameObject>>()
        {
            { PassiveItem.Rarity.normal, new Queue<GameObject>() },
            { PassiveItem.Rarity.rare, new Queue<GameObject>() },
            { PassiveItem.Rarity.unique, new Queue<GameObject>() }
        };

        // Resources ���� �� ��� ������ �ε�
        allItems = Resources.LoadAll<GameObject>("Prefabs/Items");

        // �������� ������ json �Ľ��� �������� �缳���ϱ�
        for (int i = 0; i < allItems.Length; i++)
        {
            float itemID = allItems[i].GetComponent<ItemStat>().GetItemID();
            if (Itemdict.ContainsKey(itemID))
            {
                Item itemData = Itemdict[itemID];
                var itemStat = allItems[i].GetComponent<ItemStat>();
                itemStat.SetMoveSpeed(itemData.fMoveSpeed);
                itemStat.SetMaxHP(itemData.fMaxHP);
                itemStat.SetDamage(itemData.fDamage);
                itemStat.SetBulletSpeed(itemData.fBulletSpeed);
                itemStat.SetBulletLifeTime(itemData.fBulletLifeTime);
                itemStat.SetCurrentHP(itemData.fCurrentHP);
                itemStat.SetTimeBetweenShots(itemData.timeBetweenShots);
                itemStat.SetCritical(itemData.critical);


                Debug.Log($"ItemID: {itemID}, MaxHP: {itemStat.GetMaxHP()}, MoveSpeed: {itemData.fMoveSpeed}");
            }
            else
            {
                Debug.LogWarning($"ItemDict�� itemID {itemID}�� �����ϴ�.");
            }
        }


    }

    public void InitializePoolRandomly(int poolSize)
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
                    GameObject item = Instantiate(itemToPool, transform);
                    Debug.Log(item.GetComponent<ItemStat>().GetMaxHP());
                    item.SetActive(false);
                    itemPools[testItem.type].Enqueue(item);

                }
            }
        }
    }


    public void SetItemPosition(Vector3 position)
    {
        ItemPosition = position;
    }

    public Vector3 GetItemPosition()
    {
        return ItemPosition;
    }

    public void SpawnItem()
    {
        // 60% Ȯ���� normal, 30% Ȯ���� rare, 10% Ȯ���� unique ������ ����
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
            GameObject item = selectedPool.Dequeue();
            item.transform.position = ItemPosition;
            item.SetActive(true);
        }
    }

    public void LoadSavedInventory(PlayerStat playerStat)
    {
        if (playerStat != null)
        {
            Debug.Log("Loading saved inventory...");

            for (int i = 0; i < playerStat.itemIDs.Count; i++)
            {
                float itemID = playerStat.itemIDs[i];
                Debug.Log($"{itemID}");

                foreach (var item in allItems)
                {
                    if (item.GetComponent<ItemStat>().GetItemID() == itemID)
                    {
                        GameObject newItem = Instantiate(item);
                        playerStat.items.Add(newItem);
                        newItem.SetActive(true);

                        Debug.Log($"Added item with ID {itemID} to inventory.");
                        break;
                    }
                }

            }
        }
    }


    public void ReturnItemToPool(GameObject item, PassiveItem.Rarity type)
    {
        item.SetActive(false);
        itemPools[type].Enqueue(item);
    }
}
