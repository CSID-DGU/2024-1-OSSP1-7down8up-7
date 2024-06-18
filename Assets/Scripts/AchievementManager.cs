using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public List<Achievement> AchievementsList;
    private AchievementUI ui;
    void Start()
    {
        AchievementsList = new List<Achievement>()
        {
            new JuniorBugHunter(4) //�������� Ŭ���� ����� ����� �߰����ָ� ��
        };
      
      
    }
    public void FindUI()
    {
        ui = FindAnyObjectByType<AchievementUI>();
        if (ui != null)
        {
            foreach (var achievement in AchievementsList)
            {
                achievement.SetUI(ui);
            }
        }
        else
        {
            Debug.Log("UI ��ã��");
        }
    }
    public void CheckAchievements(PlayerStat player)
    {
       
        foreach (var achievement in AchievementsList)
        {
            Debug.Log(achievement.sName);
            achievement.CheckCondition(player);
        }
    }
}
