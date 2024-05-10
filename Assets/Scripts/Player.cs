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
    
    Animator anim; // 플레이어 움직임 애니메이션

    Rigidbody2D rb;
    Collider2D coll;

    void Awake()
    {
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
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
        if (collision.CompareTag("Enemy")) // 근거리 몬스터와 접촉했을 때
        {
            GameManager.instance.health -= collision.GetComponent<MeleeATKwithNavmesh>().damage;
            Debug.Log("몬스터와 접촉 ! " + GameManager.instance.health);
        }
        // door_check 변수가 위치 기반으로 되어 있는데 door object에 collider 감지되면 TRUE로 바뀌도록

        /*
         * if (GameManager.instance.health > 0)
        {
            // .. 아직 살아있음 -> Hit Action 
        }
        else
        {
            // .. 체력이 0보다 작음 -> Die 
            Dead();
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
         */

    }

    void Dead()
    {
        GameManager.instance.GameOver();
    }
}

