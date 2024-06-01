using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    private bool isDamaged;
    private bool isDebuffed_s;
    private bool isDebuffed_a;
    
    private bool isKnockedBack;
    private Vector2 knockbackDirection;
    private float knockbackEndTime;

    [SerializeField]
    private InputActionReference attack, pointerPosition;
    private Vector2 pointerInput;
    private WeaponParent weaponParent;
    public PlayerStat playerStat;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererHead;
    private Color originalColor;

    [SerializeField]
    private float knockbackForce = 0.1f; // 넉백 힘 설정
    private float accelerationRate = 30.0f; // 가속도 설정
    private float decelerationRate = 20.0f; // 감속도 설정

    Animator anim;

    Rigidbody2D rb;
    Collider2D coll;

    void Awake()
    {
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        coll = GetComponent<Collider2D>();
        playerStat = GetComponent<PlayerStat>();
        isDamaged = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = Color.white;
        rb.drag = 5.0f; // 드래그 값 설정 (관성 줄이기)
    }

    void Update()
    {
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        float inputX = Input.GetAxisRaw("Horizontal"); // 수평이동
        float inputY = Input.GetAxisRaw("Vertical"); // 수직이동

        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);

        if (inputX != 0 || inputY != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);

        Vector2 moveTo = new Vector2(inputX, inputY).normalized;
        ApplyAcceleration(moveTo);  // 가속도 적용
        ApplyDeceleration(); // 감속 적용

        if (attack.action.triggered)
            weaponParent.Attack(pointerInput);
    }

    void FixedUpdate()
    {
        // 넉백 관련
        if (isKnockedBack)
        {
            rb.velocity = knockbackDirection * knockbackForce;

            // 넉백 시간이 끝나면 상태 초기화
            if (Time.time >= knockbackEndTime)
            {
                isKnockedBack = false;
                rb.velocity = Vector2.zero;
            }
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MeleeMonster") || collision.gameObject.CompareTag("RangedMonster") || collision.gameObject.CompareTag("BossMonster"))
        {
            if (!isDamaged)
            {
                Debug.Log("몬스터!");
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                ApplyKnockback(direction);
                playerStat.GetHarmd(collision.gameObject.GetComponent<MonsterStat>().GetDamage());
                Debug.Log(playerStat.GetPlayerStressToShow());
                if (!playerStat.IsAlive())
                {
                    Dead();
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PItem"))
        {
            // Debug.Log("아이템!");
            GameObject Pitem = collision.gameObject;
            PassiveItem Activate = Pitem.GetComponent<PassiveItem>();
            Activate.Activate();
            Pitem.SetActive(false); //지금은 SetActive False로 해두었는데 나중에 UI로 이동시키는 방법 찾아보면 될 듯해요
        }
        else if (collision.CompareTag("MonsterBullet"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(direction);
            AttackType attackType = collision.GetComponent<MonsterBullet>().attackType;
            Debug.Log(attackType.ToString());
            switch (attackType)
            {
                case AttackType.HEALTH_DECREASE:
                    {
                        if (isDamaged == false)
                        {
                            StartCoroutine(GetDamaged());
                            GameObject mBullet = collision.gameObject;
                            MonsterBullet monsterBullet = mBullet.GetComponent<MonsterBullet>();
                            playerStat.GetHarmd(collision.gameObject.GetComponent<MonsterBullet>().damage);
                        }
                    }
                    break;
                case AttackType.SPEED_DECREASE:
                    {
                        if (isDebuffed_s == false)
                        {
                            StartCoroutine(GetDebuffed_s(collision));
                            Debug.Log("몬스터 총알 ! 스피드 감소");
                        }
                    }
                    break;
                   
                case AttackType.DAMAGE_DECREASE:
                    {
                        if (isDebuffed_a == false)
                        {
                            StartCoroutine(GetDebuffed_a(collision));
                            Debug.Log("몬스터 총알 ! 공격력 감소");
                        }
                    }         
                    break;
                default:
                    break;
            }
           
            

        }
    }

    private IEnumerator GetDamaged()
    {
        isDamaged = true;
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;
       
        Debug.Log("깜빡");
        float blinkDuration = 0.5f; // Total duration of the blinking effect
        float blinkInterval = 0.1f; // Interval between blinks
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = Color.red;
            spriteRendererHead.color = Color.red;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = Color.white;
            spriteRendererHead.color = Color.white;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += 2 * blinkInterval;
        }
        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = Color.white;
        spriteRendererHead.color = Color.white;
        isDamaged = false;
    }
    private IEnumerator GetDebuffed_s(Collider2D collider)
    {
        isDebuffed_s = true;
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;
      
        float blinkDuration = 1f; // Total duration of the blinking effect
        float blinkInterval = 0.1f; // Interval between blinks
        float elapsedTime = 0f;
        StartCoroutine(playerStat.GetDebuffedSpeed(collider.gameObject.GetComponent<MonsterBullet>().damage, blinkDuration));
        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = Color.blue;
            spriteRendererHead.color = Color.blue;
            yield return new WaitForSecondsRealtime(blinkInterval);
            spriteRenderer.color = Color.white;
            spriteRendererHead.color = Color.white;
            yield return new WaitForSecondsRealtime(blinkInterval);
            elapsedTime += 2 * blinkInterval;
        }
        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = Color.white;
        spriteRendererHead.color = Color.white;
        isDebuffed_s = false;
        
    }

    private IEnumerator GetDebuffed_a(Collider2D collider)
    {
        isDebuffed_a = true;
        float blinkDuration = 1f; // Total duration of the blinking effect
        float blinkInterval = 0.1f; // Interval between blinks
        float elapsedTime = 0f;
        StartCoroutine(playerStat.GetDebuffedDamage(collider.gameObject.GetComponent<MonsterBullet>().damage, blinkDuration));
       
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;
        
        // Debug.Log("깜빡");
       
        while (elapsedTime < blinkDuration)
        {
            spriteRenderer.color = Color.magenta;
            spriteRendererHead.color = Color.magenta;
            yield return new WaitForSecondsRealtime(blinkInterval);
            spriteRenderer.color = Color.white;
            spriteRendererHead.color = Color.white;
            yield return new WaitForSecondsRealtime(blinkInterval);
            elapsedTime += 2 * blinkInterval;
        }
        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = Color.white;
        spriteRendererHead.color = Color.white;
        isDebuffed_a= false;
    }

    private void ApplyKnockback(Vector2 direction)
    {
        isKnockedBack = true;
        knockbackDirection = direction;
        knockbackEndTime = Time.time + 0.08f; // 넉백 지속 시간 설정
        // 넉백과 동시에 깜빡임 효과 시작
    }


    private void ApplyAcceleration(Vector2 direction)
    {
        // 입력이 있을 때만 가속 적용
        if (direction != Vector2.zero)
        {
            rb.AddForce(direction * accelerationRate);
        }

        // 최대 속도를 제한함
        if (rb.velocity.magnitude > playerStat.GetMoveSpeed())
        {
            rb.velocity = rb.velocity.normalized * playerStat.GetMoveSpeed();
        }
    }
    private void ApplyDeceleration()
    {
        // 입력이 없을 때 감속 적용
        if (rb.velocity != Vector2.zero)
        {
            Vector2 deceleration = -rb.velocity.normalized * decelerationRate * Time.fixedDeltaTime;
            if (deceleration.magnitude > rb.velocity.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                rb.AddForce(deceleration);
            }
        }
    }


    void Dead()
    {
        GameManager.instance.GameOver();
    }
}

