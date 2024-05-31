using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour //리소스들을 가져올 때 다루는 매니저
{
   public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path); 
    }


    public GameObject Instantiate(string path, Transform parent = null) //경로를 넣어서 리소스를 가져오는 함수
    {
        GameObject prefab = Load<GameObject>($"Prefab/{path}");
        if( prefab == null )
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        return Object.Instantiate( prefab, parent );
    }

    public void Destroy(GameObject go) //리소스를 제거하는 함수
    {
        if (go == null)
            return;

        Object.Destroy(go);
    
    }
}
