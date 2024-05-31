using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    public static Player instance; //
    private bool isDamaged;

    [SerializeField]
    private InputActionReference attack, pointerPosition;
    private Vector2 pointerInput;
    private WeaponParent weaponParent;
    public PlayerStat playerStat;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteRendererHead;
    private Color originalColor;

    Animator anim;

    Rigidbody2D rb;
    Collider2D coll;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        } //

        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        playerStat = GetComponent<PlayerStat>();
        isDamaged = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = Color.white;
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

        Vector3 moveTo = new Vector3(inputX, inputY, 0);
        // transform.position += moveTo * playerStat.GetMoveSpeed() * Time.deltaTime;
        //Debug.Log(playerStat.GetMoveSpeed());
        GetComponent<Rigidbody2D>().velocity = moveTo * playerStat.GetMoveSpeed();


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
            if (isDamaged == false)
            {
                Debug.Log("몬스터!");
                StartCoroutine(GetDamaged());
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
            float itemID = Pitem.GetComponent<ItemStat>().GetItemID();
            Activate.Activate(itemID);
            Pitem.SetActive(false); //지금은 SetActive False로 해두었는데 나중에 UI로 이동시키는 방법 찾아보면 될 듯해요
        }
        else if (collision.CompareTag("MonsterBullet"))
        {
            Debug.Log("몬스터 총알");
            if (isDamaged == false)
            {
                StartCoroutine(GetDamaged());
                playerStat.GetHarmd(collision.gameObject.GetComponent<MonsterBullet>().damage);
            }
            //Debug.Log("몬스터 총알");

        }
        else if (collision.CompareTag("StageTarget")) // stage_target과 충돌 시
        {
            FindObjectOfType<RoomInstance>().LoadNextStage();
        }
    }

    private IEnumerator GetDamaged()
    {
        spriteRendererHead = GetComponentInChildren<PlayerHead>().spriteRenderer;
        isDamaged = true;
        // Debug.Log("깜빡");
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
    void Dead()
    {
        GameManager.instance.GameOver();
    }
}


