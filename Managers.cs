using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //static�̹Ƿ� Managers�� ��ü�� ��������
    static Managers s_instance;

    //Instance�� Managers�� �����ؼ� Init�Լ��� �����ϰ� s_instance�� ��ȯ�Ѵ�.
    public static Managers Instance { get{ Init(); return s_instance; } } //�ܺο��� �Ŵ����� ������ �� �ְ� �ϴ� �Լ�

    ResourceManager _resource = new ResourceManager(); //���ҽ� �Ŵ����� ����ϱ� ���� ��ü
    //ResouceManager�� �����ؼ� Instance�� ���ҽ��� �����´�. --> Resource�� ����Ͽ� Instance._resource�� ������ �� �ְ� �ȴ�.(�̱��� ����, Instance�� ���� recource�� �����ϱ� ������)
    public static ResourceManager Resource { get { return Instance._resource; } }

    //���� ���������� DataManager�� ����ϱ� ���� ��ü�� ����� �������� �����ϰ� ����� Data�� ����
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
            //GameObject�߿��� �ϳ��� ã�ƿ��� �Լ�
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            //������Ʈ�� �������� �Լ�, DontDestroyOnLoad�� �� ���ŵ��� �ʵ��� �ϴ� �Լ���
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init(); //Manager������ �� data �ʱ�ȭ
        }

    }
}
