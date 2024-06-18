using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement
{
    public string sName { get; private set; }
    public string sDescription { get; private set; }
    public bool bIsCompleted { get; private set; }

    private AchievementUI ui;
    protected Achievement(string sName, string sDescription)
    {
        this.sName = sName;
        this.sDescription = sDescription;
        bIsCompleted = false;
    }

    public void Completed()
    {
        if (!bIsCompleted)
        {
            bIsCompleted=true;
            Debug.Log("업적 해제" + sName);
            ui?.ShowAchievement(sName,sDescription);
        }
    }
    public void SetUI(AchievementUI ui)
    {
        this.ui = ui;
        Debug.Log("UI 설정됨");
    }

    public abstract void CheckCondition(PlayerStat player);
}
