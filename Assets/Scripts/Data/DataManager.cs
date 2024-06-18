
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

    public bool fileExist;
    public string savePath;
    //저장 파일 이름
    public string fileName = "save3";


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
        nowPlayerData.critical = player1.GetComponent<PlayerStat>().GetCritical();
        nowPlayerData.score = player1.GetComponent<PlayerStat>().GetScore();

        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(savePath + fileName, data);
    }

    public void LoadData()
    {
        GameObject player1 = GameObject.Find("Player");

        try
        {
            string data = File.ReadAllText(savePath + fileName);
            nowPlayerData = JsonUtility.FromJson<PlayerData>(data);

            fileExist = true;

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
                player1.GetComponent<PlayerStat>().SetPlayerStage(nowPlayerData.playerStage);
                player1.GetComponent<PlayerStat>().SetitemCount(nowPlayerData.itemCount);
                player1.GetComponent<PlayerStat>().SetitemNames(nowPlayerData.itemIDs);
                player1.GetComponent<PlayerStat>().SetkillCount(nowPlayerData.killCount);
                player1.GetComponent<PlayerStat>().SetCritical(nowPlayerData.critical);
                player1.GetComponent<PlayerStat>().SetScore(nowPlayerData.score);
                ItemManager itemManager = GameObject.FindObjectOfType<ItemManager>();
                itemManager.LoadSavedInventory(player1.GetComponent<PlayerStat>());
            }
        }
        catch (FileNotFoundException ex)
        {
            // 파일이 존재하지 않을 때의 처리
            Console.WriteLine($"파일을 찾을 수 없습니다: {ex.Message}");
            fileExist = false;
            if (player1 != null)
            {
                player1.GetComponent<PlayerStat>().SetMoveSpeed(5);
                player1.GetComponent<PlayerStat>().SetMaxHP(100);
                player1.GetComponent<PlayerStat>().SetDamage(10);
                player1.GetComponent<PlayerStat>().SetBulletSpeed(5);
                player1.GetComponent<PlayerStat>().SetBulletLifeTime(1);
                player1.GetComponent<PlayerStat>().SetCurrentHP(60);
                player1.GetComponent<PlayerStat>().SetTimeBetweenShots((float)0.5);
                player1.GetComponent<PlayerStat>().SetPlayerStressToShow(40);
                //변경부분
                player1.GetComponent<PlayerStat>().SetPlayerStage(1);
                player1.GetComponent<PlayerStat>().SetitemCount(0);
                player1.GetComponent<PlayerStat>().SetitemNames(nowPlayerData.itemIDs);
                player1.GetComponent<PlayerStat>().SetkillCount(0);
                player1.GetComponent<PlayerStat>().SetCritical(0);
                player1.GetComponent<PlayerStat>().SetScore(1000);
            }

        }

    }

    
}


