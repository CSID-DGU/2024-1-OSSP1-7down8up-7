using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 PointerPosition { get; set; }
    public SpriteRenderer characterRenderer, weaponRenderer;
    public GameObject bulletPrefab; // Use bulletPrefab instead of bullet
    public Transform wPoint;
    public PlayerStat PlayerStat;
    public int initialPoolSize = 20; // 초기 풀의 크기 설정

    public Animator animator;

    private List<GameObject> bulletPool;
    private float timeBetweenShots;
    private float angle;
    private float shotTime;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        InitializeBulletPool(initialPoolSize);
    }

    private void Update()
    {
        // 무기가 마우스를 바라보는 방향 보게 하기
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        // 방향계산
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

    private void InitializeBulletPool(int poolSize)
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    private GameObject GetPooledBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        // 풀에 사용 가능한 총알이 없으면 새로운 총알을 생성하여 풀에 추가
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    // 공격
    public void Attack(Vector3 mousePosition)
    {
        PlayerStat = GetComponentInParent<PlayerStat>();
        Vector2 fireDirection = mousePosition - transform.position;
        timeBetweenShots = PlayerStat.GetTimeBetweenShots();
        angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        if (Time.time >= shotTime)
        {
            // 불릿 생성 (오브젝트 풀링 사용)
            GameObject newBullet = GetPooledBullet();
            if (newBullet != null)
            {
                animator.SetTrigger("Attack");

                newBullet.transform.position = wPoint.position;
                newBullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                newBullet.SetActive(true);

                Bullet bulletScript = newBullet.GetComponent<Bullet>();

                // 불릿 스탯 처리
                bulletScript.Init(PlayerStat.GetDamage(), fireDirection.normalized, PlayerStat.GetBulletSpeed(), PlayerStat.GetBulletLifeTime());
               
 

                // Set the next shot time
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}