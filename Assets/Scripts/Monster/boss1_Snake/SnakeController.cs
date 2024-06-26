using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public GameObject headPrefab;
    public List<GameObject> bodyPrefab;
    public int bodyPartCount = 6;
    private SnakeHeadController headController;

    void Start()
    {
        // 스네이크의 머리 생성
        GameObject head = Instantiate(headPrefab, transform.position, Quaternion.identity);
        head.transform.SetParent(this.transform);
        headController = head.GetComponent<SnakeHeadController>();

        // 스네이크의 몸통 생성 및 연결
        SnakePartController previousPart = headController;
        for (int i = 0; i < bodyPartCount; i++)
        {
            Vector3 spawnPosition = head.transform.position - new Vector3((i + 1) * 1, 0, 0); // 간격을 두고 생성
            GameObject bodyPart = Instantiate(bodyPrefab[i], spawnPosition, Quaternion.identity);
            bodyPart.transform.SetParent(this.transform);
            SnakeBodyController bodyController = bodyPart.GetComponent<SnakeBodyController>();

            // 앞뒤 관계 설정
            bodyController.previousBodyPart = previousPart;
            previousPart.nextBodyPart = bodyController;

            headController.RegisterBodyPart(bodyController);
            previousPart = bodyController;
        }
    }
}


