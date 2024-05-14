using UnityEngine;

public class Damage
{
    public int value;

    public Animator attacker;
    public Animator attackee;

    public Damage(int _value, Animator _attacker, Animator _attackee)
    {
        value = _value;
        attacker = _attacker;
        attackee = _attackee;
    }

    public Damage SetDamageValue(int _value)
    {
        value = _value;
        return this;
    }
}