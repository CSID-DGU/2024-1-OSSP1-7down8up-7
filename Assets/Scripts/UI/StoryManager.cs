using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public SpriteRenderer target;
    public Sprite[] sprite;
    public float waitingTime;

    private Coroutine changeSpriteCoroutine;

    private void Start()
    {
        changeSpriteCoroutine = StartCoroutine(_changeSprite());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (changeSpriteCoroutine != null)
            {
                StopCoroutine(changeSpriteCoroutine);
            }
            SceneLoader.LoadSceneHandle("Play_Stage1", 0);
        }
    }

    IEnumerator _changeSprite()
    {
        yield return new WaitForSeconds(waitingTime);
        for (int i = 0; i < sprite.Length; i++)
        {
            target.sprite = sprite[i];
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(_loadScene());
    }

    IEnumerator _loadScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneLoader.LoadSceneHandle("Play_Stage1", 0);
    }
}
