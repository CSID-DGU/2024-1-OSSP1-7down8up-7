using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShooter : MonoBehaviour
{
    public Transform playerTransform;
    private Vector2 fireDirection;
    private float angle;

    public GameObject bulletPrefab;
    public MonsterStat stat;
    public float timeBetweenShots;
    private float shotTime;
    public int initialPoolSize = 10;
    private List<GameObject> bulletPool;

    private void Start()
    {
        stat = GetComponent<MonsterStat>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        shotTime = 0f;
        InitializeBulletPool(initialPoolSize);
    }

    private void Update()
    {
        Vector2 playerPosition = playerTransform.position;
        fireDirection = playerPosition - (Vector2)transform.position;
        angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
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
        // If no inactive bullets are available, create a new one and add it to the pool
        GameObject newBullet = Instantiate(bulletPrefab, transform);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    public void Shoot()
    {
        timeBetweenShots = stat.GetTimeBetweenShots();
        if (Time.time >= shotTime)
        {
            GameObject bullet = GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                bullet.SetActive(true);

                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                MonsterBullet bulletScript = bullet.GetComponent<MonsterBullet>();
                bulletScript.damage = stat.GetDamage();
                bulletScript.speed = stat.GetBulletSpeed();
                bulletScript.lifeTime = stat.GetBulletLifeTime();

                if (bulletRb != null)
                {
                    bulletRb.velocity = fireDirection.normalized * bulletScript.speed;
                }
            }
           
            shotTime = Time.time + timeBetweenShots;
        }
    }
}
