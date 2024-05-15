using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public enum ItemType
    {
        normal,
        rare,
        unique
    }
    public ItemType type;
    Rigidbody2D rb2D;
    public GameObject player; // 플레이어 오브젝트에 대한 참조
    public GameObject GManager;
    public void OnEnable()
    {

        /////////////////죽은 몬스터 위치에 생성//////////////////////
        GManager = GameObject.Find("GManager");
        rb2D = GetComponent<Rigidbody2D>();
        
        ///////////////생성 코드 끝//////////////////////////////
    }
    public void Activate()
    {

    }
    //추가 아이템 효과를 구현해 봅시다
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
