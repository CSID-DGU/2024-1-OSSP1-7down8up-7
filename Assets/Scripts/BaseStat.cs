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
    //체력 처리 함수들
    public float GetCurrentHP()
    {
        return fCurrentHP;
    }
    public float GetMaxHP()
    {
        return fMaxHP;      
    }

    public void SetCurrentHP(float value)
    {
        fCurrentHP = value;
    }

    public void SetMaxHP(float value)
    {
        fMaxHP = value;
    }


    public bool IsAlive()
    {
        return isAlive;
    }
    public void Heal(float value) //현재 체력값 변경
    {
        fCurrentHP += value;
    }
    public void GetHarmd(float value)
    {
        fCurrentHP -= value;
        if (fCurrentHP <= 0)
        {
            isAlive = false;
        }
    }
    public void ChangeMaxHP(float value)   //최대 체력값 변경
    {
        Debug.Log(value);
        fMaxHP += value;
    }

    ////공격력 처리 함수들
    public float GetDamage()
    {
        return Mathf.Max(fDamage, 0);
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

        timeBetweenShots += value;
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
        //return Mathf.Max(fMoveSpeed, 0);
        return fMoveSpeed;
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
