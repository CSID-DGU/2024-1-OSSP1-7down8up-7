using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] // unity���� �� ���� ����
    private float moveSpeed; // �÷��̾� �̵� �ӵ�
    [SerializeField]
    private InputActionReference attack, pointerPosition; // input action
    private Vector2 pointerInput; // ���콺������ ��ġ
    private WeaponParent weaponParent;
    
    Animator anim; // �÷��̾� ������ �ִϸ��̼�
 
    void Awake()
    {
        anim = GetComponent<Animator>();
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    void Update()
    {
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
}
