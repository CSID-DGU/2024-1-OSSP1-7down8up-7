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
    [SerializeField]
    protected float critical;
    //ü�� ó�� �Լ���
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
    public void Heal(float value) //���� ü�°� ����
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
            //���Ͱ� �׾��� �� killcount ����
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                //���� ��
                player.GetComponent<PlayerStat>().PlusKillCount();
                //���� ���� �� ���� 10����
                player.GetComponent<PlayerStat>().ChangeScore(10);
            }
        }
    }
    public void ChangeMaxHP(float value)   //�ִ� ü�°� ����
    {
        Debug.Log(value);
        fMaxHP += value;
        fCurrentHP += value;
    }

    ////���ݷ� ó�� �Լ���
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

    //�̵��ӵ� ó�� �Լ���
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
