using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum MonsterType
// {
//     Melee = 0,
//     Range = 1,
//     Debuff = 2,
// }

// [Serializable]
// public class Monster 
// {
//     public MonsterType Type;
//     public int MaxHP;
//     public int Speed;
//     public int Power;
//     public string MonsterPrefabPath;
//     public Status MonsterStatus;

//     public Monster getCopy()
//     {
//         return (Monster)this.MemberwiseClone();
//     }
// }

// [Serializable]
// public class MonsterList
// {
//     public Dictionary<int, Monster> monsters;
//     public MonsterList() 
//     {
//         monsters = new Dictionary<int, Monster>();
//     }
// }

[System.Serializable]
public class MonsterData
{
    public List<MonsterDataItem> monsterData;
}

[System.Serializable]
public class MonsterDataItem
{
    public string ID;
    public string Type;
    public string MaxHP;
    public string Speed;
    public string Power;
    public string MonsterPrefabPath;
}

public enum MonsterType
{
    Melee = 0,
    Range = 1,
    Debuff = 2
}

public class Monster
{
    public int ID;
    public MonsterType Type;
    public int MaxHP;
    public int Speed;
    public int Power;
    public string MonsterPrefabPath;
    //public Status MonsterStatus;
}