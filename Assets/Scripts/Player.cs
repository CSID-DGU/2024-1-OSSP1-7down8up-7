using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] // unity에서 값 변경 가능
    private float moveSpeed; // 플레이어 이동 속도
    [SerializeField]
    private InputActionReference attack, pointerPosition; // input action
    private Vector2 pointerInput; // 마우스포인터 위치
    private WeaponParent weaponParent;
    private PlayerStat PlayerStat;
    Animator anim; // 플레이어 움직임 애니메이션

    Rigidbody2D rb;
    Collider2D coll;

    void Awake()
    {
       
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        PlayerStat = GetComponent<PlayerStat>();
    }

    void Update()
    {
        moveSpeed=PlayerStat.GetMoveSpeed();
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
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        if (attack.action.triggered)
            weaponParent.Attack(pointerInput);

        
}

private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PItem"))
        {
            Debug.Log("아이템!");
            GameObject Pitem = collision.gameObject;
            PassiveItem Activate=Pitem.GetComponent<PassiveItem>();
            Activate.Activate();
            Pitem.SetActive(false); //지금은 SetActive False로 해두었는데 나중에 UI로 이동시키는 방법 찾아보면 될 듯해요
        }
    }

    void Dead()
    {
        Debug.Log("Dead");
        //GameManager.instance.GameOver();
    }
}

