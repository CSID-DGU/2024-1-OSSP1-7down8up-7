using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   BoxCollider2D collider2D;
    Rigidbody2D rigidbody2D;
    private string sTargetDoorTag;

    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        // �±׸� ������� Ÿ�� ���� �±׸� ����
        SetTargetDoorTag();
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
            default:
                sTargetDoorTag = "door1";
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("�÷��̾� �浹 ����");
            GameObject closestTarget = FindClosestTarget(sTargetDoorTag, collision.transform.position);

            if (closestTarget != null)
            {
                Debug.Log("���� ����� �� ã��");
                Vector3 newPosition = GetNewPosition(closestTarget.transform.position);
                collision.transform.position = newPosition;
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
                newPosition.y -= 1.5f;
                break;
            case "door1":
                newPosition.x -= 1;
                break;
            case "door2":
                newPosition.y += 1;
                break;
            default:
                newPosition.x += 1;
                break;
        }
        return newPosition;
    }
}
