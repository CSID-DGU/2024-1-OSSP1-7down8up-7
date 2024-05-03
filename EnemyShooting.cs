using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float attackRate;
    public float timeAfterAttack;

    void Start() 
    {
        timeAfterAttack = 0f;
    }

    void Update(){
        timeAfterAttack += Time.deltaTime;

        if(timeAfterAttack >= attackRate) 
        {
            timeAfterAttack = 0f;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
        // StartCoroutine(Bullet());
    }

    // IEnumerator Bullet(){
    //     Instantiate(bullet, transform.position, transform.rotation);
    //     yield return new WaitForSeconds(3.0f);
    //     StartCoroutine(Bullet());
    // }
}
