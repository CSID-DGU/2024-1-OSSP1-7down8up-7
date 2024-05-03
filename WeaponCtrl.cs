using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform wPoint;
    public float timeBetweenShots;

    private float shotTime;

    void Update() 
    {
        //카메라 스크린의 마우스 거리와 총과의 방향
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        //마우스 거리로부터 각도 계산
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        //축으로부터 방향과 각도의 회전값
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        //마우스 왼쪽 버튼을 눌렀을 때
        if (Input.GetMouseButton(0)) 
        {
            if (Time.time >= shotTime)
            {
                //총알 생성
                Instantiate(bullet, wPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                //재장전 총알 딜레이
                shotTime = Time.time +timeBetweenShots;
            }
        }
    }
}
