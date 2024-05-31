using System;
using System.Collections;
using System.Collections.Generic;
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
    private List<float> itemIDs = new List<float>();
    public List<GameObject> items;
    private Inventory inventory;
   

    public void Start()
    {
        base.enabled = true;
        inventory = FindObjectOfType<Inventory>();
    }

    public void AddItemToInventory(GameObject item)
    {
        items.Add(item);
        //inventory.FreshSlot();
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
        killCount += 1;
    }

}



