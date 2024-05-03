/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        return dict;
    }
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}



public class DataManager
{

    public Dictionary<int, Stat> StatDict {  get; private set; } = new Dictionary<int, Stat>();
    public void init()
    {


        StatDict = LoadJson<StatData, int, Stat>("StatData") MakeDict();
        

        

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
*/