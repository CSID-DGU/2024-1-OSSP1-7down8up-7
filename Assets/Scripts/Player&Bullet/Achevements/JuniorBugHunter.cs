using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuniorBugHunter : Achievement
{
    private int iRequiredKills;

    public JuniorBugHunter(int iRequiredKills):base("Junior Bug Hunter",$"���� 5���� �ذ��߽��ϴ�")
    {
        this.iRequiredKills = iRequiredKills;
    }

    public override void CheckCondition(PlayerStat player)
    {
        if (!bIsCompleted&&player.GetkillCount() > iRequiredKills) {
            Completed();
        }
    }
}
