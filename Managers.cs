using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //static이므로 Managers의 객체가 유일해짐
    static Managers s_instance;

    //Instance는 Managers에 접근해서 Init함수를 실행하고 s_instance을 반환한다.
    public static Managers Instance { get{ Init(); return s_instance; } } //외부에서 매니저를 가져올 수 있게 하는 함수

    ResourceManager _resource = new ResourceManager(); //리소스 매니저를 사용하기 위한 객체
    //ResouceManager에 접근해서 Instance의 리소스를 가져온다. --> Resource를 사용하여 Instance._resource를 가져올 수 있게 된다.(싱글톤 적용, Instance에 대한 recource만 접근하기 위함임)
    public static ResourceManager Resource { get { return Instance._resource; } }

    //위와 마찬가지로 DataManager를 사용하기 위해 객체를 만들고 전역으로 간단하게 사용할 Data를 만듬
    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {

    }


    static void Init()
    {
        if(s_instance == null)
        {
            //GameObject중에서 하나를 찾아오는 함수
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            //컴포넌트를 가져오는 함수, DontDestroyOnLoad는 잘 제거되지 않도록 하는 함수임
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init(); //Manager가져올 때 data 초기화
        }

    }
}
