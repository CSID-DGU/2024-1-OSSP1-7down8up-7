using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    private bool isDamaged;
    public bool isDebuffed_s = false;
    public bool isDebuffed_a = false;

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
    private float knockbackForce = 0.5f; 
    private float knockbackDuration = 0.08f; 
    private float accelerationRate = 15.0f; 
    private float decelerationRate = 20.0f; 

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
        rb.drag = 5.0f; 
    }

    void Update()
    {
        if (isKnockedBack)
        {
            rb.AddForce(knockbackDirection * knockbackForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            Debug.Log("넉백!");
            
            if (Time.time >= knockbackEndTime)
            {
                Debug.Log("넉백끝!");
                isKnockedBack = false;

            }
        }
        pointerInput = GetPointerInput();
        weaponParent.PointerPosition = pointerInput;
        float inputX = Input.GetAxisRaw("Horizontal"); 
        float inputY = Input.GetAxisRaw("Vertical"); 

        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);

        if (inputX != 0 || inputY != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);

        Vector2 moveTo = new Vector2(inputX, inputY).normalized;
        ApplyAcceleration(moveTo);  
        ApplyDeceleration(); 

        if (attack.action.triggered)
            weaponParent.Attack(pointerInput);
       

    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MeleeMonster") || collision.gameObject.CompareTag("RangedMonster"))
        {
            if (!isDamaged)
            {
                Debug.Log("몬스터!");
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                ApplyKnockback(direction);
                Debug.Log(direction);
                playerStat.GetHarmd(collision.gameObject.GetComponent<MonsterStat>().GetDamage());
               
                if (!playerStat.IsAlive())
                {
                    Dead();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pitem"))
        {
            GameObject Pitem = collision.gameObject;
            PassiveItem Activate = Pitem.GetComponent<PassiveItem>();
            float itemID = Pitem.GetComponent<ItemStat>().GetItemID();
            Activate.Activate(itemID);
            //추가부분->아이템 추가
            playerStat.AddItemToInventory(Pitem);
           
            Pitem.SetActive(false);
        }
        else if (collision.CompareTag("MonsterBullet"))
        {

            Vector2 direction = (transform.position - collision.transform.position).normalized;


            
            GameObject mBullet = collision.gameObject;
            MonsterBullet monsterBullet = mBullet.GetComponent<MonsterBullet>();
            AttackType attackType = monsterBullet.attackType;
            switch (attackType)
            {
                case AttackType.HEALTH_DECREASE:
                    if (isDamaged == false)
                    {
                        ApplyKnockback(direction);
                        playerStat.GetHarmd(monsterBullet.damage);
                    }
                    break;
                case AttackType.SPEED_DECREASE:
                    if (isDebuffed_s == false)
                    {
                        StartCoroutine(GetDebuffed_s(collision));
                    }
                    break;
                case AttackType.DAMAGE_DECREASE:
                    if (isDebuffed_a == false)
                    {
                        StartCoroutine(GetDebuffed_a(collision));
                    }
                    break;
            }
        }
    }

    private void ApplyKnockback(Vector2 direction)
    {

        knockbackDirection = direction;
        knockbackEndTime = Time.time + knockbackDuration;
        isKnockedBack = true;
        StartCoroutine(GetDamaged());

    }

    private IEnumerator GetDamaged()
    {
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;
        isDamaged = true;

        float blinkDuration = 1.0f; 
        float blinkInterval = 0.2f; 
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
           
            spriteRenderer.color = (elapsedTime % blinkInterval < blinkInterval / 2) ? Color.red : Color.white;
            spriteRendererHead.color = (elapsedTime % blinkInterval < blinkInterval / 2) ? Color.red : Color.white;

            yield return null; 
            elapsedTime += Time.deltaTime;
        }

      
        spriteRenderer.color = Color.white;
        spriteRendererHead.color = Color.white;
        isDamaged = false;
    }

    private IEnumerator GetDebuffed_s(Collider2D collider)
    {
        isDebuffed_s = true;
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;

        float blinkDuration = 1f; 
        float blinkInterval = 0.1f; 
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
        isDebuffed_a = false;
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
