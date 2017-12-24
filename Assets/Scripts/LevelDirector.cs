using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelDirector : MonoBehaviour
{
    private static LevelDirector instance;
    public static LevelDirector Instance
    {
        get
        {
            if (instance == null)
            {
                throw new NullReferenceException("No LevelDirector in scene");
            }
            return instance;
        }
    }
    [SerializeField]
    private MainPlane mainPlane;
    [SerializeField]
    private PlayerData date;

    private GameObject bossPlane;
    private int score;
    private int maxScore;
    private int playerLifeCount = 3;
    [SerializeField]
    private GameOverMenu gameOverMenu;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (maxScore < score)
            {
                maxScore = value;
                date.maxScore = value;
            }
        }
    }
    public int MaxScore { get { return maxScore; } }
    public int PlayerLifeCount { get { return playerLifeCount; } }
    public MainPlane CurrentPlane { get; private set; }

    public Action GameStartAction;
    public Action GameOverAction;

    private void Awake()
    {
        instance = this;
        Init();
    }

    private void Start()
    {
        if (GameStartAction != null)
            GameStartAction();
        StartCoroutine(Decorate());
    }

    private void Init()
    {
        mainPlane = Resources.Load<MainPlane>("Prefabs/MainPlane");
        bossPlane = Resources.Load<GameObject>("Prefabs/Enemys/Boss");
        date = Resources.Load<PlayerData>("PlayerData");

        maxScore = date.maxScore;
    }

    private IEnumerator Decorate()
    {
        //几时生成
        yield return new WaitForSeconds(2);

        //生成什么
        CurrentPlane = Instantiate(mainPlane, mainPlane.transform.position, Quaternion.identity);
        CurrentPlane.OnDeadEvent += OnMainPlaneDead;
    }

    //当主角满足以下条件时执行死亡代码
    private void OnMainPlaneDead()
    {
        playerLifeCount--;
        if (playerLifeCount > 0)
        {
            StartCoroutine(Decorate());
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (GameOverAction != null)
            GameOverAction();
    }
}
