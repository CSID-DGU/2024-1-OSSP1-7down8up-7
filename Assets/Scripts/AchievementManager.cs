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
            new JuniorBugHunter(4) //도전과제 클래스 만들고 여기다 추가해주면 됨
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
            Debug.Log("UI 못찾음");
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
