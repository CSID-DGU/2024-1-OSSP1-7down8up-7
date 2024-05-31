using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatUI : MonoBehaviour
{
    // Reference to the PlayerStat script
    public PlayerStat playerStat;

    [SerializeField]
    private Slider HPbar;

    public Image HPbarImage;
    public Sprite nomalHPSprite;
    public Sprite lowHPSprite;

    // UI Text elements
    public TMP_Text hpText;
    public TMP_Text moveSpeedText;
    public TMP_Text damageText;
    public TMP_Text bulletSpeedText;
    public TMP_Text bulletLifeTimeText;

    public TMP_Text moveSpeedChangeText;
    public TMP_Text damageChangeText;
    public TMP_Text bulletSpeedChangeText;
    public TMP_Text bulletLifeTimeChangeText;

    private float currentMoveSpeed;
    private float currentDamage;
    private float currentBulletSpeed;
    private float currentBulletLifeTime;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (HPbar.value >= 0.7f)
            HPbarImage.sprite = lowHPSprite;
        else
            HPbarImage.sprite = nomalHPSprite;
        // Update the UI every frame
        UpdateUI();
    }

    void UpdateUI()
    {
        float moveSpeedChange = playerStat.GetMoveSpeed() - currentMoveSpeed;
        float damageChange = playerStat.GetDamage() - currentDamage;
        float bulletSpeedChange = playerStat.GetTimeBetweenShots() - currentBulletSpeed;
        float bulletLifeTimeChange = playerStat.GetBulletLifeTime() - currentBulletLifeTime;

        // Display stat changes
        if (moveSpeedChange != 0)
            StartCoroutine(DisplayChangeText(moveSpeedChangeText, moveSpeedChange));
        else if (damageChange != 0)
            StartCoroutine(DisplayChangeText(damageChangeText, damageChange));
        else if (bulletSpeedChange != 0)
            StartCoroutine(DisplayChangeText(bulletSpeedChangeText, bulletSpeedChange));
        else if (bulletLifeTimeChange != 0)
            StartCoroutine(DisplayChangeText(bulletLifeTimeChangeText, bulletLifeTimeChange));

        currentMoveSpeed = playerStat.GetMoveSpeed();
        currentDamage = playerStat.GetDamage();
        currentBulletSpeed = playerStat.GetTimeBetweenShots();
        currentBulletLifeTime = playerStat.GetBulletLifeTime();


        HPbar.value = playerStat.GetPlayerStressToShow() / playerStat.GetMaxHP();
        hpText.text = playerStat.GetPlayerStressToShow().ToString("F1") + " / " + playerStat.GetMaxHP().ToString("F1");
        moveSpeedText.text = "이동속도: " + currentMoveSpeed.ToString("F1");
        damageText.text = "공격력: " + currentDamage.ToString("F1");
        bulletSpeedText.text = "공격 쿨타임: " + currentBulletSpeed.ToString("F1");
        bulletLifeTimeText.text = "사거리: " + currentBulletLifeTime.ToString("F1");
    }

    IEnumerator DisplayChangeText(TMP_Text changeText, float changeAmount)
    {
        if (changeAmount < 0)
        {
            if (changeText == bulletSpeedChangeText)
                changeText.color = Color.green;
            else
                changeText.color = Color.red;
        }
        else if (changeAmount > 0)
        {
            if (changeText == bulletSpeedChangeText)
                changeText.color = Color.red;
            else
                changeText.color = Color.green;
        }

        // Display change amount
        changeText.text = (changeAmount > 0 ? "+" : "") + changeAmount.ToString("F1");

        // Show text
        changeText.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(1.0f);

        // Hide text
        changeText.gameObject.SetActive(false);
    }
}
