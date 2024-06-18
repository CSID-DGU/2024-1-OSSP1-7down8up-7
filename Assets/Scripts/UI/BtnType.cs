using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum BTNType
    {
        NewGame,
        Continue,
        ResumeGame,
        GoToMain,
        Quit
    }

    public BTNType currentType;
    Vector3 defaultScale;
    private void Start()
    {
        defaultScale = this.transform.localScale;
    }
    public void OnBtnClick()
    {
        DataManager stagedata = new DataManager();
        stagedata.Init();
        stagedata.LoadData();
        int stage = stagedata.nowPlayerData.playerStage;
        Debug.Log("Clicked button: " + currentType);
        switch (currentType)
        {
            case BTNType.NewGame:
                SceneManager.LoadScene("Story");
                break;
            case BTNType.Continue:
                if(stagedata.fileExist)
                {
                    if (stage == 1)
                    {
                        SceneLoader.LoadSceneHandle("Play_stage1", 1);
                    }
                    else if (stage == 2)
                    {
                        SceneLoader.LoadSceneHandle("Play_stage2", 1);
                    }
                    else if (stage == 3)
                    {
                        SceneLoader.LoadSceneHandle("Play_stage3", 1);
                    }
                }
                else
                {
                    Debug.Log("세이브 파일이 없습니다.");
                }
                

                break;
            case BTNType.Quit:
                Debug.Log("종료");
                Application.Quit();
                break;
            case BTNType.ResumeGame:
                PauseMenu.Instance.ResumeGame();
                break;
            case BTNType.GoToMain:
                Time.timeScale = 1;
                GameObject player = GameObject.Find("Player");
                if (player != null)
                    Destroy(player);
                SceneLoader.LoadSceneHandle("Main", 1);
                break;


        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale = defaultScale;
    }
}
