using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [SerializeField]
    

    private float fPlayerStressToShow;

    public void Start()
    {
        base.enabled = true;
        fCurrentHP = 100;
        fMaxHP = 100;
        fPlayerStressToShow=fMaxHP-fCurrentHP;
    }

   

    public void UpdatePlayerStressToShow()
    {
        fPlayerStressToShow = fMaxHP - fCurrentHP;
    }
    public float GetPlayerStressToShow()
    { 
        return fPlayerStressToShow; 
    }
}
