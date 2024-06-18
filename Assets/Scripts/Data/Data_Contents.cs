using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region MonsterStat
//Json으로 바로 가져올 수 있게 해주는 것, 파일 포멧임, Json으로부터 불러드릴 데이터 형태를 미리 잡아두는 것
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
    public float fMoveSpeed; //이동속도
    public float fMaxHP;     //최대 체력
    public float fDamage;    //데미지
    public float fBulletSpeed; //총알 스피드
    public float fBulletLifeTime;  //총알 지속시간
    public float fCurrentHP;   //현재 체력
    public float timeBetweenShots;
    public float fPlayerStressToShow;
    //변경 부분
    public int playerStage = 1;
    public int itemCount;
    public List<float> itemIDs = new List<float>();
    public int killCount;
    public float critical;
    public float score;
    
}



#endregion
