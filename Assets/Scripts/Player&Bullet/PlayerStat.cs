using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [SerializeField]


    private float fPlayerStressToShow = 0;

    //변경 부분
    [SerializeField]
    private int playerStage = 1;
    [SerializeField]
    private int itemCount;
    [SerializeField]
    protected int killCount;
    [SerializeField]
    protected float score;

    [SerializeField]
    public List<float> itemIDs = new List<float>();
    public List<GameObject> items;
    private Inventory inventory;

    private AchievementManager manager;

    public void Start()
    {
        base.enabled = true;
        inventory = FindObjectOfType<Inventory>();
        manager = FindObjectOfType<AchievementManager>();
    }

    public void AddItemToInventory(GameObject item)
    {
        items.Add(item);
    }

    public bool ContainsAll(float i1, float i2, float i3)
    {
        if (itemIDs.Contains(i1)&& itemIDs.Contains(i2)&& itemIDs.Contains(i3))
        {
            return true;
        }

        return false;
    }
    public bool ContainsAll(float i1, float i2)
    {
        if (itemIDs.Contains(i1) && itemIDs.Contains(i2))
        {
            return true;
        }

        return false;
    }


    public IEnumerator GetDebuffedSpeed(float value, float debuffDuration)
    {
        float fOriginSpeed = this.fMoveSpeed;
        ChangeMoveSpeed(-value);
        yield return new WaitForSecondsRealtime(debuffDuration);
        this.fMoveSpeed = fOriginSpeed;
    }



    public IEnumerator GetDebuffedDamage(float value, float debuffDuration)
    {
        float fOriginDamage = this.fDamage;
        ChangeDamage(-value);
        yield return new WaitForSecondsRealtime(debuffDuration);
        this.fDamage = fOriginDamage;

    }
    public void UpdatePlayerStressToShow()
    {
        fPlayerStressToShow = fMaxHP - fCurrentHP;
    }
    public float GetPlayerStressToShow()
    {
        UpdatePlayerStressToShow();
        return fPlayerStressToShow;
    }

    public void SetPlayerStressToShow(float value)
    {
        fPlayerStressToShow = value;
    }

    //아래로 변경부분
    public int GetPlayerStage()
    {
        return playerStage;
    }


    public void SetPlayerStage(int value)
    {
        playerStage = value;
    }
    public float GetScore()
    {
        return score;
    }


    public void SetScore(float value)
    {
        score = value;
    }
    public void ChangeScore(float value)
    {
        score += value;
    }

    public int GetitemCount()
    {
        return itemCount;
    }

    public void SetitemCount(int value)
    {
        itemCount = value;
    }
    public void PlusitemCount()
    {
        itemCount += 1;
    }
    public void PlusitemName(float itemName)
    {
        itemIDs.Add(itemName);
    }

    public List<float> GetitemNames()
    {
        return itemIDs;
    }

    public void SetitemNames(List<float> itemNames1)
    {
        itemIDs = itemNames1;
        Debug.Log(itemIDs);
    }
    public int GetkillCount()
    {
        return killCount;
    }

    public void SetkillCount(int value)
    {
        killCount = value;
    }

    public void PlusKillCount()
    {
        Debug.Log("+");
        killCount += 1;
        Heal(3);
        manager.CheckAchievements(this);
    }

}



