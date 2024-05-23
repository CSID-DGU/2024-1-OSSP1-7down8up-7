
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
    public Dictionary<int, Stat> MonsterStatDict { get; private set; } = new Dictionary<int, Stat>();
    public Dictionary<float, Item> ItemDict { get; private set; } = new Dictionary<float, Item>();
    public PlayerData nowPlayerData = new PlayerData();

    string savePath;
    //저장 파일 이름
    string fileName="save1";
    
    
    public void Init()
    {
        
        savePath = Application.persistentDataPath+"/";
        MonsterStatDict = LoadJson<MonsterStatData, int, Stat>("MonsterData").MakeDict(); //Json을 읽어서 Dictionary화 함
        ItemDict = LoadJson<ItemData, float, Item>("ItemData").MakeDict();
        //SaveData();
        //Debug.Log(nowPlayerData.playerpos.position);

    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void SaveData()
    {
        //현재는 위치만 저장, 추후 기능 추가
        GameObject player1 = GameObject.Find("Player");
        nowPlayerData.playerpos = player1.transform;

        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(savePath+fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(savePath+fileName);
        nowPlayerData = JsonUtility.FromJson<PlayerData>(data);
    }
}


