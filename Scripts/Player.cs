using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] // unity���� �� ���� ����
    private float moveSpeed;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
