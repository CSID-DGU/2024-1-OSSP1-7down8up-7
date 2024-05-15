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
    public GameObject player; // �÷��̾� ������Ʈ�� ���� ����
    public GameObject GManager;
    public void OnEnable()
    {

        /////////////////���� ���� ��ġ�� ����//////////////////////
        GManager = GameObject.Find("GManager");
        rb2D = GetComponent<Rigidbody2D>();
        
        ///////////////���� �ڵ� ��//////////////////////////////
    }
    public void Activate()
    {

    }
    //�߰� ������ ȿ���� ������ ���ô�
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
