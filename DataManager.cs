
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;




public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}



public class DataManager
{
    //읽기는 pulic, 쓰기는 private로 설정
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>();
    public PlayerData nowPlayerData = new PlayerData();

    string savePath;
    string fileName="save1";
    
    
    public void Init()
    {
        savePath = Application.persistentDataPath+"/";
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict(); //Json을 읽어서 Dictionary화 함
        SaveData();
        LoadData();
    
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> //Json을 읽어서 반환하는 함수
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(savePath+fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(savePath+fileName);
        nowPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }
}


