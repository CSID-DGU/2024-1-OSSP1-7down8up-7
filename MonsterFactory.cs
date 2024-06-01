using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//factory method
public abstract class MonsterFactory<T> : MonoBehaviour
{
    public Monster Spawn(T _type, Vector3 _position, Transform _parentTransform, Dictionary<int, Stat> Monsterdict)
    {
        Monster monster = Create(_type, Monsterdict);
        monster.transform.SetParent(_parentTransform, false);
        monster.transform.localPosition = _position;
        return monster;
    }
    protected abstract Monster Create(T _type, Dictionary<int, Stat> Monsterdict);
}