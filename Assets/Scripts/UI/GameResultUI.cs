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
        bugCountText.text = "���� ���� ��: " + stat.GetkillCount().ToString("F0");
        totalScoreText.text = "����: " + stat.GetScore().ToString("F0");
        
        if (endingType == 0)
        {
            resultTitleText.text = "���� ����";
            endingText.text = "����� ������ ��ġ�� ���߽��ϴ�...";
            endingImage.sprite = ending_0;
        }
        else if (endingType == 1)
        {
            resultTitleText.text = "���� Ŭ����!";
            endingText.text = "����� ���׸� ��ġ��� �� �����߽��ϴ�. ...�Ƹ���\ntip: �� ���� ���׸� ��ƺ�����";
            endingImage.sprite = ending_1;
        }
        else if (endingType == 2)
        {
            resultTitleText.text = "���� Ŭ����!";
            endingText.text = "����� ��� ���׸� ��ġ��� �� �����߽��ϴ�! ������ ������!";
            endingImage.sprite = ending_2;
        }

        Destroy(player);
    }
}
