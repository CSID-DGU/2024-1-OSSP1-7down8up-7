using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    Rigidbody2D rb2D;
    public GameObject player; // �÷��̾� ������Ʈ�� ���� ����

    public void Activate()
    {
        this.gameObject.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("Player object is not set for PassiveItem.");
            return;
        }
       
        rb2D = GetComponent<Rigidbody2D>();
        Vector2 playerPosition = player.transform.position; // �÷��̾��� ���� ��ġ

        // �÷��̾� �ֺ� 1ĭ ���� ������ ���� ��ġ ����
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // �÷��̾� ��ġ�� ���� ��ġ�� ���� ���� ��ġ ����
        rb2D.position = playerPosition + randomPosition;

        
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
