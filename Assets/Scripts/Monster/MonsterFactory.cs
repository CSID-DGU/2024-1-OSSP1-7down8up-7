using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//factory method
public abstract class MonsterFactory<T> : MonoBehaviour
{
    public Monster Spawn(T _type, GameObject _parent)
    {
        Monster monster = Create(_type);
        monster.transform.SetParent(_parent.transform, false);

        float randomX = Random.Range(_parent.transform.position.x - 7f, _parent.transform.position.x + 7f);
        float randomY = Random.Range(_parent.transform.position.y - 3f, _parent.transform.position.y + 3f);

        Vector3 randomPosition = new Vector3(randomX, randomY, _parent.transform.position.z);

        monster.transform.localPosition = randomPosition;
        return monster;
    }
    protected abstract Monster Create(T _type);
}


