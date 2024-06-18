using UnityEngine;

public class MonsterCollisionManager : MonoBehaviour
{
    public int maxMonsters = 3;  // BoxCollider2D 내에서 허용되는 몬스터의 최대 수
    public float pushBackForce = 5f;  // 몬스터를 밀어내는 힘의 크기

    void OnCollisionStay2D(Collision2D collision)
    {
        // 충돌 객체가 몬스터인지 확인
        if (collision.gameObject.CompareTag("MeleeMonster") || collision.gameObject.CompareTag("RangedMonster") || collision.gameObject.CompareTag("DebuffMonster"))
        {
            // 현재 BoxCollider2D 내에 있는 모든 몬스터 가져오기
            Collider2D[] monsters = Physics2D.OverlapBoxAll(GetComponent<CircleCollider2D>().bounds.center, GetComponent<CircleCollider2D>().bounds.size, 0f, LayerMask.GetMask("Monster"));

            // 몬스터 수가 제한을 초과하는지 확인
            if (monsters.Length > maxMonsters)
            {
                // 초과하는 몬스터를 밀어내기
                PushBackExceedingMonsters(monsters);
            }
        }
    }

    void PushBackExceedingMonsters(Collider2D[] monsters)
    {
        for (int i = maxMonsters; i < monsters.Length; i++)
        {
            Rigidbody2D monsterRb = monsters[i].GetComponent<Rigidbody2D>();
            if (monsterRb != null)
            {
                Vector2 pushDirection = (monsters[i].transform.position - transform.position).normalized;
                monsterRb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
            }
        }
    }
}
