using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SpawnPoint : MonoBehaviour
{
    private Transform point;

    public List<SpawnObject> SpawnObjects = new List<SpawnObject>();

    [NonSerialized]
    // public MonsterPatrolArea patrolArea;

    // SpawnObject가 나올 수 있는 확률을 누적해 더해놓은 것.
    public float[] SpawnProbAccum;

    private void Awake()
    {
        point = this.GetComponent<Transform>();

        if (transform.childCount > 0)
        {
            // patrolArea = GetComponentInChildren<MonsterPatrolArea>();
        }
    }

    [Serializable]
    public class SpawnObject
    {
        public int ID;
        public float SpawnProbablity;
        // 오브젝트 풀링으로 미리 생성해 놓을 디폴트 오브젝트 갯수. 유니티 에디터에서 셋팅
        // 몬스터 종류에 따라, 미리 생성해 놓는 메리트가 적은 (보스 몬스터, 1개만 존재하는 퀘스트 아이템 등) 경우 1로 셋팅.
        public int PoolingValue;

    }
}