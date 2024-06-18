using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text achievementText;
    public float displayDuration = 10f; // 텍스트를 표시할 시간(초)

    public void ShowAchievement(string achievementName,string description)
    {
        if (achievementText != null)
        {
            achievementText.text = "도전과제 달성!\n" + achievementName + "\n" + description;
            StartCoroutine(HideTextAfterDelay(displayDuration));
          
        }
        else
        {
            Debug.LogError("Achievement Text is not assigned.");
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        achievementText.text = "";
    }

}
