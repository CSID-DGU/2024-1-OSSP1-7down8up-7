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

    //_name: 로딩할 씬 이름, _loadType: 0 - 새게임 / 1 - 이어하기
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

            if (loadType == 0) // 새게임. 플레이어 및 맵 초기화 필요
                Debug.Log("new Game");
            if (loadType == 1) // 이어하기. 기존 데이터 로드 필요
                Debug.Log("continue Game");

            if (progressbar.value < 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            else if (progressbar.value >= 0.9f)
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
        }
        operation.allowSceneActivation = true;
    }
}
