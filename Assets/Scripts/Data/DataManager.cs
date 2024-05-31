
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
    string fileName = "save3";


    public void Init()
    {

        savePath = Application.persistentDataPath + "/";
        MonsterStatDict = LoadJson<MonsterStatData, int, Stat>("MonsterData").MakeDict(); //Json을 읽어서 Dictionary화 함
        ItemDict = LoadJson<ItemData, float, Item>("ItemData").MakeDict();


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
        nowPlayerData.fMoveSpeed = player1.GetComponent<PlayerStat>().GetMoveSpeed();
        nowPlayerData.fMaxHP = player1.GetComponent<PlayerStat>().GetMaxHP();
        nowPlayerData.fDamage = player1.GetComponent<PlayerStat>().GetDamage();
        nowPlayerData.fBulletSpeed = player1.GetComponent<PlayerStat>().GetBulletSpeed();
        nowPlayerData.fBulletLifeTime = player1.GetComponent<PlayerStat>().GetBulletLifeTime();
        nowPlayerData.fCurrentHP = player1.GetComponent<PlayerStat>().GetCurrentHP();
        nowPlayerData.timeBetweenShots = player1.GetComponent<PlayerStat>().GetTimeBetweenShots();
        nowPlayerData.fPlayerStressToShow = player1.GetComponent<PlayerStat>().GetPlayerStressToShow();
        //변경 부분
        nowPlayerData.playerStage = player1.GetComponent<PlayerStat>().GetPlayerStage();
        nowPlayerData.itemCount = player1.GetComponent<PlayerStat>().GetitemCount();
        nowPlayerData.itemIDs = player1.GetComponent<PlayerStat>().GetitemNames();
        nowPlayerData.killCount = player1.GetComponent<PlayerStat>().GetkillCount();

        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(savePath + fileName, data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(savePath + fileName);
        nowPlayerData = JsonUtility.FromJson<PlayerData>(data);


        GameObject player1 = GameObject.Find("Player");
        if (player1 != null)
        {
            player1.GetComponent<PlayerStat>().SetMoveSpeed(nowPlayerData.fMoveSpeed);
            player1.GetComponent<PlayerStat>().SetMaxHP(nowPlayerData.fMaxHP);
            player1.GetComponent<PlayerStat>().SetDamage(nowPlayerData.fDamage);
            player1.GetComponent<PlayerStat>().SetBulletSpeed(nowPlayerData.fBulletSpeed);
            player1.GetComponent<PlayerStat>().SetBulletLifeTime(nowPlayerData.fBulletLifeTime);
            player1.GetComponent<PlayerStat>().SetCurrentHP(nowPlayerData.fCurrentHP);
            player1.GetComponent<PlayerStat>().SetTimeBetweenShots(nowPlayerData.timeBetweenShots);
            player1.GetComponent<PlayerStat>().SetPlayerStressToShow(nowPlayerData.fPlayerStressToShow);
            //변경부분
            player1.GetComponent<PlayerStat>().SetPlayerStage(nowPlayerData.playerStage);
            player1.GetComponent<PlayerStat>().SetitemCount(nowPlayerData.itemCount);
            player1.GetComponent<PlayerStat>().SetitemNames(nowPlayerData.itemIDs);
            player1.GetComponent<PlayerStat>().SetkillCount(nowPlayerData.killCount);

        }

    }
}


