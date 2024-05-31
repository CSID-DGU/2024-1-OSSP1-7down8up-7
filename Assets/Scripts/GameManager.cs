using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int playerId;
    [Header("# Game Object")]
    public Player player;
    public ItemManager itemManager;

    private int iBasicItemPoolSize = 8;


    ResourceManager _resource;
    public static ResourceManager Resource { get { return instance._resource; } }
    public DataManager _data = new DataManager();
    public static DataManager Data { get { return instance._data; } }

    void Awake()
    {
        isLive = false;
        itemManager = GetComponentInChildren<ItemManager>();
        _resource = GetComponent<ResourceManager>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        GameStart(1);
        _data.Init();
        itemManager.Init(); //�߰�
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void FixedUpdate()
    {
        if (isLive == true && player.isDebuffed_a == false && player.isDebuffed_s == false)
        {
            // _data.SaveData();
        }

    }

    // ���� �ε�� �� ȣ��Ǵ� �Լ�
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("���� �ε�Ǿ����ϴ�: " + scene.name);
        // ���� �ε�Ǿ��� �� ������ �۾� �߰�
        if (scene.name == "Play_Stage1" && SceneLoader.loadType == 1)
        {
            isLive = true;
            player = GameObject.Find("Player").GetComponent<Player>();
            PlayerStat stat = GameObject.Find("Player").GetComponent<PlayerStat>();
            _data.LoadData();
            itemManager.InitializePoolRandomly(iBasicItemPoolSize - stat.GetComponent<PlayerStat>().GetitemCount()); //Ǯ ���� ���̴� �ڵ�


        }
        else if (scene.name == "Play_Stage1" && SceneLoader.loadType == 0)
        {
            isLive = true;
            player = GameObject.Find("Player").GetComponent<Player>();
            PlayerStat stat = GameObject.Find("Player").GetComponent<PlayerStat>();
            stat.SetBulletLifeTime(5f);
            stat.SetBulletSpeed(5f);
            stat.SetCurrentHP(100f);
            stat.SetDamage(10f);
            stat.SetMaxHP(100f);
            stat.SetMoveSpeed(5f);
            stat.SetTimeBetweenShots(0.5f);
            stat.SetitemCount(0);
            itemManager.InitializePoolRandomly(iBasicItemPoolSize);  //�߰�

        }
    }

    public void GameStart(int id)
    {

        //player.gameObject.SetActive(true);
        // uiLevelUp.Select(playerId % 2);
        Resume();
        // AudioManager.instance.PlayBgm(true);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        // uiResult.gameObject.SetActive(true);
        // uiResult.Lose();
        Stop();

        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        // enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        // uiResult.gameObject.SetActive(true);
        // uiResult.Win();
        Stop();

        // AudioManager.instance.PlayBgm(false);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void Stop()
    {
        isLive = false;
        //Time.timeScale = 0;
        // uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        // uiJoy.localScale = Vector3.one;
    }

    void OnDestroy()
    {
        // �� �ε� �̺�Ʈ ��� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
