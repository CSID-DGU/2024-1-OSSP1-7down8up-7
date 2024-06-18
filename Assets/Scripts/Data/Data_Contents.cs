using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region MonsterStat
//Json���� �ٷ� ������ �� �ְ� ���ִ� ��, ���� ������, Json���κ��� �ҷ��帱 ������ ���¸� �̸� ��Ƶδ� ��
[Serializable]
public class Stat
{
    public int ID;
    public String MonsterName;
    public String Desc;
    public float fMaxHP;
    public float fCurrentHP;
    public float fDamage;
    public float AttackDistance;
    public float DetectionDistance;
    public bool IsAttackin;

    public float fMoveSpeed;
    public float fBulletSpeed;
    public float fBulletLifeTime;
    public float timeBetweenShots;
}

[Serializable]
public class MonsterStatData : ILoader<int, Stat>
{
    public List<Stat> monsterData = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in monsterData)
            dict.Add(stat.ID, stat);
        return dict;
    }
}

#endregion


#region Item
[Serializable]
public class Item
{
    public float itemID;
    public bool isAlive = true;
    public float fMoveSpeed;
    public float fMaxHP;
    public float fDamage;
    public float fBulletSpeed;
    public float fBulletLifeTime;
    public float fCurrentHP;
    public float timeBetweenShots;
    public float critical;

}

[Serializable]
public class ItemData : ILoader<float, Item>
{
    public List<Item> itemData = new List<Item>();

    public Dictionary<float, Item> MakeDict()
    {
        Dictionary<float, Item> dict = new Dictionary<float, Item>();
        foreach (Item type in itemData)
            dict.Add(type.itemID, type);
        return dict;
    }
}

#endregion

#region PlayerStat
[Serializable]
public class PlayerData
{
    public int itemID;
    public bool isAlive = true;
    public float fMoveSpeed; //�̵��ӵ�
    public float fMaxHP;     //�ִ� ü��
    public float fDamage;    //������
    public float fBulletSpeed; //�Ѿ� ���ǵ�
    public float fBulletLifeTime;  //�Ѿ� ���ӽð�
    public float fCurrentHP;   //���� ü��
    public float timeBetweenShots;
    public float fPlayerStressToShow;
    //���� �κ�
    public int playerStage = 1;
    public int itemCount;
    public List<float> itemIDs = new List<float>();
    public int killCount;
    public float critical;
    public float score;
    
}



#endregion
