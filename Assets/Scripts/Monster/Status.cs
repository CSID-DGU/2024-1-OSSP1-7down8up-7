using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    private int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;

            // if (PlayerInfoSystem.Instance != null)
            // {
            //     PlayerInfoSystem.Instance.PlayerInfoWindowUpdate();
            // }
        }
    }

    // 몬스터가 활성화될 때 호출해, status를 초기화.
    public void StatusInit(int MaxHP)
    {
        CurrentHP = MaxHP;
        
    }

    // 최종적인 데미지를 계산하고
    // UI에 입힌 데미지를 표시하며, 공격 이펙트를 불러와 재생한다 
    public void CalculateDamage(Damage damage)
    {
        if (CurrentHP - damage.value >= 0)
        {
            CurrentHP -= damage.value;
        }
        else
        {
            CurrentHP = 0;
        }

        // DamageIndicator.mInstance.CallFloatingText(damage.SetDamageValue(resultDamage));

        // damage.attacker.SetBool("DamagedProcessed", true);
    }

    // 공격 데미지 공식
    public Damage DecideDamageValue(Animator attacker, Animator attackee)
    {
        int damageValue = 1;
        return new Damage((damageValue), attacker, attackee);
    }

}