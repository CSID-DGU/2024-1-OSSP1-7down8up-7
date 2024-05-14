using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    Rigidbody2D rb2D;
    public GameObject player; // 플레이어 오브젝트에 대한 참조

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
        Vector2 playerPosition = player.transform.position; // 플레이어의 현재 위치

        // 플레이어 주변 1칸 범위 내에서 랜덤 위치 생성
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // 플레이어 위치에 랜덤 위치를 더해 최종 위치 설정
        rb2D.position = playerPosition + randomPosition;

        
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
