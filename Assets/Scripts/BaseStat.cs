using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    [SerializeField]
    protected bool isAlive = true;
    [SerializeField]
    protected float fMoveSpeed; //이동속도
    [SerializeField]
    protected float fMaxHP;     //최대 체력
    [SerializeField]
    protected float fDamage;    //데미지
    [SerializeField]
    protected float fBulletSpeed; //총알 스피드
    [SerializeField]
    protected float fBulletLifeTime;  //총알 지속시간
    [SerializeField]
    protected float fCurrentHP;   //현재 체력
    [SerializeField]
    protected float timeBetweenShots;
    [SerializeField]
    protected float critical;
    //체력 처리 함수들
    public float GetCurrentHP()
    {
        return fCurrentHP;
    }
    public float GetMaxHP()
    {
        return fMaxHP;      
    }
    public void PlusMaxHP(float value)
    {
        fMaxHP += value;
    }

    public void SetCurrentHP(float value)
    {
        fCurrentHP = value;
    }

    public void SetMaxHP(float value)
    {
        fMaxHP = value;
    }
    public float GetCritical()
    {
        return critical;
    }
    public void SetCritical(float value)
    {
        critical = value;
    }
    public void PlusCritical(float value)
    {
        critical += value;
        if(critical>=1.0)
        {
            critical = (float)1.0;
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }
    public void Heal(float value) //현재 체력값 변경
    {
        fCurrentHP += value;
        if (fCurrentHP >= fMaxHP)
        {
            fCurrentHP = fMaxHP;
        }
    }
    public void GetHarmd(float value)
    {
        fCurrentHP -= value;
        if (fCurrentHP <= 0)
        {
            isAlive = false;
            
        }
    }
    public void GetMonsterHarmd(float value)
    {
        fCurrentHP -= value;
        if (fCurrentHP <= 0)
        {
            isAlive = false;
            //몬스터가 죽었을 때 killcount 증가
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                //몬스터 죽
                player.GetComponent<PlayerStat>().PlusKillCount();
                //몬스터 죽일 때 점수 10증가
                player.GetComponent<PlayerStat>().ChangeScore(10);
            }
        }
    }
    public void ChangeMaxHP(float value)   //최대 체력값 변경
    {
        Debug.Log(value);
        fMaxHP += value;
        fCurrentHP += value;
    }

    ////공격력 처리 함수들
    public float GetDamage()
    {
        return Mathf.Max(fDamage, 0);
    }
    public void PlusDamage(float value)
    {
        fDamage += value;
    }
    public float GetBulletSpeed()
    {
        return Mathf.Max(fBulletSpeed, 0);
    }
    public float GetBulletLifeTime()
    {
        return Mathf.Max(fBulletLifeTime, 0);
    }
    public float GetTimeBetweenShots()
    {
        return Mathf.Max(timeBetweenShots, 0);
    }


    public void SetDamage(float value)
    {
        fDamage = value;
    }
    public void SetBulletSpeed(float value)
    {
        fBulletSpeed = value;
    }
    public void SetBulletLifeTime(float value)
    {
        fBulletLifeTime = value;
    }
    public void SetTimeBetweenShots(float value)
    {
        timeBetweenShots = value;
    }
    public void ChangeTimeBetweenShots(float value)
    {

        timeBetweenShots -= value;
        if (timeBetweenShots <= 0.1f)
        {
            timeBetweenShots = 0.1f;
        }
    }
    public void ChangeDamage(float value)
    {



        fDamage += value;

    }
    public void ChangeBulletSpeed(float value)
    {


        fBulletSpeed += value;

    }
    public void ChangeBulletLifeTime(float value)
    {
        fBulletLifeTime += value;
    }

    //이동속도 처리 함수들
    public float GetMoveSpeed()
    {
        return Mathf.Max(fMoveSpeed, 0);
    }

    public void SetMoveSpeed(float value)
    {
        fMoveSpeed = value;
    }

    public void ChangeMoveSpeed(float value)
    {
        fMoveSpeed += value;
    }


}
