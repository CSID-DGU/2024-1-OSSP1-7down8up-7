using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerStat stat;

    public TMP_Text resultTitleText;
    public TMP_Text bugCountText;
    public TMP_Text totalScoreText;
    public TMP_Text endingText;
    public Image endingImage;

    public Sprite ending_0;
    public Sprite ending_1;
    public Sprite ending_2;

    void Start()
    {
        player = GameObject.Find("Player");
        stat = player.GetComponent<PlayerStat>();
        Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        inventory.FreshSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGameResult(int endingType)
    {
        bugCountText.text = "잡은 버그 수: " + stat.GetkillCount().ToString("F0");
        totalScoreText.text = "총점: " + stat.GetScore().ToString("F0");
        
        if (endingType == 0)
        {
            resultTitleText.text = "게임 오버";
            endingText.text = "당신은 오류를 고치지 못했습니다...";
            endingImage.sprite = ending_0;
        }
        else if (endingType == 1)
        {
            resultTitleText.text = "게임 클리어!";
            endingText.text = "당신은 버그를 해치우는 데 성공했습니다. ...아마도\ntip: 더 많은 버그를 잡아보세요";
            endingImage.sprite = ending_1;
        }
        else if (endingType == 2)
        {
            resultTitleText.text = "게임 클리어!";
            endingText.text = "당신은 모든 버그를 해치우는 데 성공했습니다! 진정한 개발자!";
            endingImage.sprite = ending_2;
        }

        Destroy(player);
    }
}
