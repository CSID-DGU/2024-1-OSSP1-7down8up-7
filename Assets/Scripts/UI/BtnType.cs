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
        Debug.Log("Clicked button: " + currentType);
        switch (currentType)
        {
            case BTNType.NewGame:
                SceneManager.LoadScene("Story");
                break;
            case BTNType.Continue:
                SceneLoader.LoadSceneHandle("Play_stage1", 1);
                break;
            case BTNType.Quit:
                Debug.Log("Á¾·á");
                Application.Quit();
                break;
            case BTNType.ResumeGame:
                PauseMenu.Instance.ResumeGame();
                break;
            case BTNType.GoToMain:
                PauseMenu.Instance.GoToMain();
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
