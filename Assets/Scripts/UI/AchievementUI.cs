using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text achievementText;
    public float displayDuration = 10f; // �ؽ�Ʈ�� ǥ���� �ð�(��)

    public void ShowAchievement(string achievementName,string description)
    {
        if (achievementText != null)
        {
            achievementText.text = "�������� �޼�!\n" + achievementName + "\n" + description;
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
