
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
    //�б�� pulic, ����� private�� ����
    public Dictionary<int, Stat> MonsterStatDict { get; private set; } = new Dictionary<int, Stat>();
    public Dictionary<float, Item> ItemDict { get; private set; } = new Dictionary<float, Item>();
    public PlayerData nowPlayerData = new PlayerData();

    string savePath;
    //���� ���� �̸�
    string fileName = "save3";


    public void Init()
    {

        savePath = Application.persistentDataPath + "/";
        MonsterStatDict = LoadJson<MonsterStatData, int, Stat>("MonsterData").MakeDict(); //Json�� �о Dictionaryȭ ��
        ItemDict = LoadJson<ItemData, float, Item>("ItemData").MakeDict();


    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void SaveData()
    {
        //����� ��ġ�� ����, ���� ��� �߰�
        GameObject player1 = GameObject.Find("Player");
        nowPlayerData.fMoveSpeed = player1.GetComponent<PlayerStat>().GetMoveSpeed();
        nowPlayerData.fMaxHP = player1.GetComponent<PlayerStat>().GetMaxHP();
        nowPlayerData.fDamage = player1.GetComponent<PlayerStat>().GetDamage();
        nowPlayerData.fBulletSpeed = player1.GetComponent<PlayerStat>().GetBulletSpeed();
        nowPlayerData.fBulletLifeTime = player1.GetComponent<PlayerStat>().GetBulletLifeTime();
        nowPlayerData.fCurrentHP = player1.GetComponent<PlayerStat>().GetCurrentHP();
        nowPlayerData.timeBetweenShots = player1.GetComponent<PlayerStat>().GetTimeBetweenShots();
        nowPlayerData.fPlayerStressToShow = player1.GetComponent<PlayerStat>().GetPlayerStressToShow();
        //���� �κ�
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
            //����κ�
            player1.GetComponent<PlayerStat>().SetPlayerStage(nowPlayerData.playerStage);
            player1.GetComponent<PlayerStat>().SetitemCount(nowPlayerData.itemCount);
            player1.GetComponent<PlayerStat>().SetitemNames(nowPlayerData.itemIDs);
            player1.GetComponent<PlayerStat>().SetkillCount(nowPlayerData.killCount);

        }

    }
}


