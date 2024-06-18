using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Slider progressbar;
    public static string loadScene;
    public static int loadType;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    //_name: �ε��� �� �̸�, _loadType: 0 - ������ / 1 - �̾��ϱ�
    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("Loading");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);

        while (!operation.isDone)
        {
            yield return null;

            if (loadType == 0) // ������. �÷��̾� �� �� �ʱ�ȭ �ʿ�
                Debug.Log("new Game");
            if (loadType == 1) // �̾��ϱ�. ���� ������ �ε� �ʿ�
                Debug.Log("continue Game");

            if (progressbar.value < 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            else if (progressbar.value >= 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
        }
        operation.allowSceneActivation = true;
    }
}
