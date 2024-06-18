using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    public PlayerStat stat;
    public ItemManager itemManager;
    public GameResultUI gameResultUI;

    public AudioClip bossAudioClip1;
    public AudioClip bossAudioClip2;
    public AudioClip bossAudioClip3;
    public AudioClip roomAudioClip1;
    public AudioClip roomAudioClip2;
    public AudioClip roomAudioClip3;

    public AchievementManager achievementManager;

    private int iBasicItemPoolSize = 8;

    ResourceManager _resource;
    public static ResourceManager Resource { get { return instance._resource; } }
    public DataManager _data = new DataManager();
    public static DataManager Data { get { return instance._data; } }

    void Awake()
    {
        achievementManager = GetComponentInChildren<AchievementManager>();
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
        _data.Init();
        itemManager.Init(); //추가
        // 씬 로드 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    IEnumerator SaveData()
    {
        while (true)
        {
            _data.SaveData();
            yield return new WaitForSecondsRealtime(10f);
        }
    }

    // 씬이 로드될 때 호출되는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬이 로드되었습니다: " + scene.name);
        if (scene.name == "Main")
        {
            isLive = false;
        }
        else if (SceneLoader.loadType == 1)
        {
            
            GameObject player1 = GameObject.Find("Player");
            if (player1 != null)
            {
                player = player1.GetComponent<Player>();  // Player 참조 설정
                stat = player.GetComponent<PlayerStat>();
            }
            _data.LoadData();
            achievementManager.FindUI();
            isLive = true;
            itemManager.InitializePoolRandomly(iBasicItemPoolSize - stat.GetitemCount());
            StartCoroutine(SaveData());
        }
        else if (scene.name == "Play_Stage1" && SceneLoader.loadType == 0)
        {
          
            GameObject player1 = GameObject.Find("Player");
            if (player1 != null)
            {
              
                player = player1.GetComponent<Player>();  // Player 참조 설정
                stat = player.GetComponent<PlayerStat>();
                stat.SetBulletLifeTime(0.6f);
                stat.SetBulletSpeed(5f);
                stat.SetCurrentHP(100f);
                stat.SetDamage(8.5f);
                stat.SetMaxHP(100f);
                stat.SetMoveSpeed(4f);
                stat.SetTimeBetweenShots(0.5f);
                stat.SetScore(1000);
                stat.SetitemCount(0);
                stat.itemIDs.Clear();
                stat.items.Clear();
                StartCoroutine(SaveData());
            }
            itemManager.InitializePoolRandomly(iBasicItemPoolSize - stat.GetitemCount());
            achievementManager.FindUI();
            isLive = true;
        }
    }

    public void GameStart(int id)
    {
        Resume();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return null;

        SceneManager.LoadScene("GameResult");

        yield return null;

        gameResultUI = FindObjectOfType<GameResultUI>();
        gameResultUI.SetGameResult(0);

    
        File.Delete(Path.Combine(_data.savePath, _data.fileName));

        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;

        yield return null;

        SceneManager.LoadScene("GameResult");

        yield return null;

        gameResultUI = FindObjectOfType<GameResultUI>();

        if (stat.GetkillCount() >= 200)
            gameResultUI.SetGameResult(2);
        else
            gameResultUI.SetGameResult(1);

        File.Delete(Path.Combine(_data.savePath, _data.fileName));

        Stop();
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
        {
            StopCoroutine(SaveData());
            return;
        }


        if (stat != null)
        {
            stat.ChangeScore(-(float)1.0 * Time.deltaTime);
        }
    }

    public void Stop()
    {
        isLive = false;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    void OnDestroy()
    {
        // 씬 로드 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
