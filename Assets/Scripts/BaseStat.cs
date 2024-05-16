using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat : MonoBehaviour
{
    [SerializeField]
    protected bool isAlive = true;
    [SerializeField]
    protected float fMoveSpeed; //�̵��ӵ�
    [SerializeField]
    protected float fMaxHP;     //�ִ� ü��
    [SerializeField]
    protected float fDamage;    //������
    [SerializeField]
    protected float fBulletSpeed; //�Ѿ� ���ǵ�
    [SerializeField]
    protected float fBulletLifeTime;  //�Ѿ� ���ӽð�
    [SerializeField]
    protected float fCurrentHP;   //���� ü��
    [SerializeField]
    protected float timeBetweenShots;
    //ü�� ó�� �Լ���
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
    public void Heal(float value) //���� ü�°� ����
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
    public void ChangeMaxHP(float value)   //�ִ� ü�°� ����
    {
        fMaxHP += value;
    }

    ////���ݷ� ó�� �Լ���
    public float GetDamage()
    {
        return fDamage;
    }
    public float GetBulletSpeed()
    {
        return fBulletSpeed;
    }
    public float GetBulletLifeTime()
    {
        return fBulletLifeTime;
    }
    public float GetTimeBetweenShots()
    {
        return timeBetweenShots;
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

    //�̵��ӵ� ó�� �Լ���
    public float GetMoveSpeed()
    {
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
