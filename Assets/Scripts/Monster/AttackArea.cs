using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Status attackerStatus;
    private new Collider2D collider;
    private Animator attacker;
    private Animator attackee;

    public delegate void HandleAttackEvent(ref Damage damage);
    public HandleAttackEvent handleAttackEvent;

    private void Awake()
    {
        attackerStatus = GetComponentInParent<Status>();
        gameObject.GetComponentInParent<Animator>();
        collider = GetComponent<Collider2D>();
        attacker = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 어떤 경우에도, 한 공격 모션에 데미지가 한 번만 들어가게 한다.
        // 그렇게 하기 위해 Animator 파라미터로 'DamagedProcessed' 를 만들어 사용함
        // 하지만, 이렇게 하면 플레이어가 다수의 몬스터를 한 번에 공격할 수 없으므로 수정이 필요함
        //if (attacker.GetBool("DamagedProcessed") == true) return;

        // attackee = other.gameObject.GetComponent<Animator>();
        // attackeeStatus = other.gameObject.GetComponentInParent<Status>();
        // Damage damage = attackerStatus.DecideDamageValue(attacker, attackee);
        // this.handleAttackEvent(ref damage);
        //attackerStatus.CalculateDamage(damage);
    }

    public void OnAttack()
    {
        collider.enabled = true;
    }

    public void OffAttack()
    {
        collider.enabled = false;
    }
}