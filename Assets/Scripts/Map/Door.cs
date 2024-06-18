using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    BoxCollider2D collider2D;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    public string sTargetDoorTag;
    public GameObject closestTarget;
    public Sprite OpenDoor;
    MonsterManager monsterManager;

    AudioClip bossAudioClip1;
    AudioClip bossAudioClip2;
    AudioClip bossAudioClip3;
    AudioClip roomAudioClip1;
    AudioClip roomAudioClip2;
    AudioClip roomAudioClip3;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        monsterManager = FindClosestTarget("Room", transform.position).GetComponentInChildren<MonsterManager>();
        SetTargetDoorTag();

        bossAudioClip1 = GameObject.Find("GameManager").GetComponent<GameManager>().bossAudioClip1;
        bossAudioClip2 = GameObject.Find("GameManager").GetComponent<GameManager>().bossAudioClip2;
        bossAudioClip3 = GameObject.Find("GameManager").GetComponent<GameManager>().bossAudioClip3;
        roomAudioClip1 = GameObject.Find("GameManager").GetComponent<GameManager>().roomAudioClip1;
        roomAudioClip2 = GameObject.Find("GameManager").GetComponent<GameManager>().roomAudioClip2;
        roomAudioClip3 = GameObject.Find("GameManager").GetComponent<GameManager>().roomAudioClip3;

    }

    void SetTargetDoorTag()
    {
        switch (gameObject.tag)
        {
            case "door0":
                sTargetDoorTag = "door2";
                break;
            case "door1":
                sTargetDoorTag = "door3";
                break;
            case "door2":
                sTargetDoorTag = "door0";
                break;
            case "door3":
                sTargetDoorTag = "door1";
                break;
            case "bossDoor0":
                sTargetDoorTag = "bossDoor2";
                break;
            case "bossDoor1":
                sTargetDoorTag = "bossDoor3";
                break;
            case "bossDoor2":
                sTargetDoorTag = "bossDoor0";
                break;
            case "bossDoor3":
                sTargetDoorTag = "bossDoor1";
                break;
        }
    }

    private void Update()
    {
        if (monsterManager == null || monsterManager.monsters.Count <= 0)
            spriteRenderer.sprite = OpenDoor;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(monsterManager == null || monsterManager.monsters.Count <= 0) 
        {
            spriteRenderer.sprite = OpenDoor;
            if (collision.gameObject.tag == "Player")
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    closestTarget = FindClosestTarget(sTargetDoorTag, collision.transform.position);

                    int currentType = transform.parent.GetComponent<RoomInstance>().type;
                    int stage = GameObject.Find("Player").GetComponent<PlayerStat>().GetPlayerStage();
                    //보스방에서 일반방으로 갈때 사운드 클립 변경
                    if (currentType == 2)
                    {
                        if (closestTarget.transform.parent.GetComponent<RoomInstance>().type == 0)
                        {

                            GameObject sound = GameObject.Find("Sound");
                            AudioSource audioSource = sound.GetComponent<AudioSource>();
                            if (audioSource != null && bossAudioClip1 != null)
                            {

                                if (stage == 1)
                                {
                                    // AudioClip 변경
                                    audioSource.clip = roomAudioClip1;
                                    audioSource.loop = true;
                                    // 변경된 AudioClip을 재생
                                    audioSource.Play();
                                }
                                else if (stage == 2)
                                {
                                    // AudioClip 변경
                                    audioSource.clip = roomAudioClip2;
                                    audioSource.loop = true;
                                    // 변경된 AudioClip을 재생
                                    audioSource.Play();
                                }
                                else if (stage == 3)
                                {
                                    // AudioClip 변경
                                    audioSource.clip = roomAudioClip3;

                                    audioSource.loop = true;
                                    // 변경된 AudioClip을 재생
                                    audioSource.Play();
                                }

                            }
                        }
                    }
                    //보스 방일 경우 사운드 클립 변경
                    if (closestTarget.transform.parent.GetComponent<RoomInstance>().type == 2)
                    {

                        GameObject sound = GameObject.Find("Sound");
                        AudioSource audioSource = sound.GetComponent<AudioSource>();
                        if (audioSource != null && bossAudioClip1 != null)
                        {
                            if (stage == 1)
                            {
                                // AudioClip 변경
                                audioSource.clip = bossAudioClip1;
                                audioSource.loop = true;
                                // 변경된 AudioClip을 재생
                                audioSource.Play();
                            }
                            else if (stage == 2)
                            {
                                // AudioClip 변경
                                audioSource.clip = bossAudioClip2;
                                audioSource.loop = true;
                                // 변경된 AudioClip을 재생
                                audioSource.Play();
                            }
                            else if (stage == 3)
                            {
                                // AudioClip 변경
                                audioSource.clip = bossAudioClip3;

                                audioSource.loop = true;
                                // 변경된 AudioClip을 재생
                                audioSource.Play();
                            }

                        }
                    }

                    if (closestTarget != null)
                    {
                        Vector3 newPosition = GetNewPosition(closestTarget.transform.position);
                        collision.transform.position = newPosition;
                    }
                }

            }
        }
       
    }

    GameObject FindClosestTarget(string tag, Vector3 playerPosition)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        float closestDistance = float.MaxValue;
        GameObject closestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector2.Distance(playerPosition, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    Vector3 GetNewPosition(Vector3 targetPosition)
    {
        Vector3 newPosition = targetPosition;
        switch (gameObject.tag)
        {
            case "door0":
                newPosition.y -= 1;
                sTargetDoorTag = "door2";
                break;
            case "door1":
                newPosition.x -= 1;
                sTargetDoorTag = "door3";
                break;
            case "door2":
                newPosition.y += 1;
                sTargetDoorTag = "door0";
                break;
            case "door3":
                newPosition.x += 1;
                sTargetDoorTag = "door1";
                break;
            case "bossDoor0":
                newPosition.y -= 1;
                sTargetDoorTag = "bossDoor2";
                break;
            case "bossDoor1":
                newPosition.x -= 1;
                sTargetDoorTag = "bossDoor3";
                break;
            case "bossDoor2":
                newPosition.y += 1;
                sTargetDoorTag = "bossDoor0";
                break;
            case "bossDoor3":
                newPosition.x += 1;
                sTargetDoorTag = "bossDoor1";
                break;
        }
        return newPosition;
    }
}
