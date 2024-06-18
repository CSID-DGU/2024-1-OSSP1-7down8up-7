using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour //���ҽ����� ������ �� �ٷ�� �Ŵ���
{
   public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path); 
    }


    public GameObject Instantiate(string path, Transform parent = null) //��θ� �־ ���ҽ��� �������� �Լ�
    {
        GameObject prefab = Load<GameObject>($"Prefab/{path}");
        if( prefab == null )
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        return Object.Instantiate( prefab, parent );
    }

    public void Destroy(GameObject go) //���ҽ��� �����ϴ� �Լ�
    {
        if (go == null)
            return;

        Object.Destroy(go);
    
    }
}
