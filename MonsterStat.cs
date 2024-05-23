using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : BaseStat
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public string MonsterName;
    [SerializeField]
    public string Desc;
    [SerializeField]
    private float fAttackDistance;
    [SerializeField]
    private float fDetectionDistance;
    public void SetMonsterName(string value)
    {
        MonsterName = value;
    }
    public string GetMonsterName()
    {
        return MonsterName;
    }
    public void SetDesc(string value)
    {
        Desc = value;
    }
    public string GetDesc()
    {
        return Desc;
    }

    public void SetAttackDistance(float value)
    {
        fAttackDistance = value;
    }
    public float GetAttackDistance()
    {
        return fAttackDistance;
    }

    public void SetDetectionDistance(float value)
    {
        fDetectionDistance = value;
    }
    public float GetDetectionDistance()
    {
        return fDetectionDistance;
    }
}
