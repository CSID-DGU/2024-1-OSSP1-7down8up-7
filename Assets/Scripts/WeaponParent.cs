using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }
    public SpriteRenderer characterRenderer, weaponRenderer;
    public GameObject bullet;
    public Transform wPoint;
    [SerializeField]
    public float timeBetweenShots;

    private float angle;
    private float shotTime;

    private void Update()
    {
        // 무기가 마우스 포인터 방향을 바라보도록 함
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        // 공격 방향
        Vector2 fireDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
            scale.y = -1;
        else if (direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        else
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
    }

    // 공격
    public void Attack(Vector3 mousePosition)
    {
        Vector2 fireDirection = mousePosition - transform.position;
        angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        if (Time.time >= shotTime)
        {
            //총알 생성
            Instantiate(bullet, wPoint.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
            //재장전 총알 딜레이
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
