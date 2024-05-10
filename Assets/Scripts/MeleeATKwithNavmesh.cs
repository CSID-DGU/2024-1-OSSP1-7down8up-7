using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeATKwithNavmesh : MonoBehaviour
{
    Coroutine MoveCoroutine;

    public Rigidbody2D rb2D;
    public CircleCollider2D circleCollider;
    NavMeshAgent agent;
    public GameObject player;

    public bool isFoundAlan = false;

    Vector3 destPosition;
    public float damage;         //���ݷ�
    public float speed;         //�ӵ�
    public float health;        //ü��
    public float maxHealth;     //�ִ� ü��
    public RuntimeAnimatorController[] animCon;
    SpriteRenderer spriter;
    bool isLive = true;


    public float detectionRange = 5f; // �÷��̾� ���� ����
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true;

        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(WanderRoutine());
    }

 
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= detectionRange)
            {
                isFoundAlan = true;
                destPosition = player.transform.position;
                Move();  // ������ ����
            }
            else
            {
                isFoundAlan = false;
            }
        }
    }
    public IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!isFoundAlan)
            {
                    ChooseNewdestPosition();
                
                yield return new WaitForSeconds(2f);
            }
            else
            {
                if (IsAtDestination())
                {
                    destPosition = player.transform.position;  // �÷��̾� ��ġ ����
                    Move();
                }
                yield return new WaitForSeconds(0.1f); // �÷��̾� ���� ������ ª�� ����
            }
        }
    }
    bool IsAtDestination()
    {
        // NavMeshAgent�� ���� ��ġ�� ������ ������ �Ÿ��� Ȯ��
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath;
    }

    void ChooseNewdestPosition()
    {
        Vector3 randomDirection = RandomDirection();
        Vector3 randomDestination = transform.position + randomDirection * Random.Range(1, 3);
        NavMeshHit hit;  // NavMesh ���ø� ����� ������ ����ü

        // �ִ� 2 ���� ���� ������ ���� ����� walkable ��ġ�� ã��
        if (NavMesh.SamplePosition(randomDestination, out hit, 2.0f, NavMesh.AllAreas))
        {
            destPosition = hit.position;
        }
        else
        {
            // Walkable ��ġ�� ã�� ���� ��� ���� ��ġ�� ����
            destPosition = transform.position;
        }
        Move();
    }

    Vector3 RandomDirection()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }
    public void Move()
    {
        if (isFoundAlan)
        {
            agent.stoppingDistance = 0.3f;
        }
        else
        {
            agent.stoppingDistance = 0.7f;
        }
        agent.SetDestination(destPosition);
    }


    /////////////////////////���⼭���� ���ݰ���

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) // �浹�� collision�� Bullet������ ���� Ȯ��
            return;

        health -= collision.GetComponent<Bullet>().damage; //Bullet ��ũ��Ʈ ������Ʈ���� damage�� �����ͼ� ü�¿��� ��´�.
        Debug.Log("���� ���� ! ");

        if (health > 0)
        {
            // .. ���� ������� -> Hit Action 
        }
        else
        {
            // .. ü���� 0���� ���� -> Die 
            Dead();
            //GameManager.instance.kill++;
            //GameManager.instance.GetExp();
        }

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}



