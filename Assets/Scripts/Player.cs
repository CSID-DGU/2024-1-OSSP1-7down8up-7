using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] // unity���� �� ���� ����
    private float moveSpeed; // �÷��̾� �̵� �ӵ�
    [SerializeField]
    private InputActionReference attack, pointerPosition; // input action
    private Vector2 pointerInput; // ���콺������ ��ġ
    private WeaponParent weaponParent;
    private PlayerStat PlayerStat;
    Animator anim; // �÷��̾� ������ �ִϸ��̼�

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
        float inputX = Input.GetAxisRaw("Horizontal"); // �����̵�
        float inputY = Input.GetAxisRaw("Vertical"); // �����̵�

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
            Debug.Log("������!");
            GameObject Pitem = collision.gameObject;
            PassiveItem Activate=Pitem.GetComponent<PassiveItem>();
            Activate.Activate();
            Pitem.SetActive(false); //������ SetActive False�� �صξ��µ� ���߿� UI�� �̵���Ű�� ��� ã�ƺ��� �� ���ؿ�
        }
    }

    void Dead()
    {
        Debug.Log("Dead");
        //GameManager.instance.GameOver();
    }
}

