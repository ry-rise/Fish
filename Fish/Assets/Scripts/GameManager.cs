﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    //時間
    [SerializeField]
    private Text timeText = null;
    [SerializeField]
    private float timeMax = 100;
    private float time = 0;
    //ライフ
    [SerializeField]
    private Text lifeText = null;

    [SerializeField]
    private float maxLife = 1;
    private float life;
    public float Life { get { return life; } }
    [SerializeField]
    private float damage = 1;

    //レベル＆サイズ
    [SerializeField]
    private Text levelText = null;
    [SerializeField]
    private float firstLevelUpSize = 10f;
    [SerializeField]
    private float multipleA = 1.1f;
    [SerializeField]
    private float multipleB = 15;
    private float currentExp;
    public int CurrentLevel { get; private set; }

    private GameObject player;
    private Camera mainCamera;

    [SerializeField]
    private Vector3 cameraBasePosition = Vector3.zero;

    private List<string> eatFishTypes;
    private Dictionary<string, int> eatFishes;

    public static float GetTime = 0;//時間
    public static List<string> GetEatFishTypes = null; //食べた魚の名前
    public static Dictionary<string, int> GetEatFishes = null;//食べた魚の数

    private void EatFishCounter(string fishName)
    {
        if(eatFishTypes.Any(a => a == fishName))
        {
            ++eatFishes[fishName];
        }
        else
        {
            eatFishTypes.Add(fishName);
            eatFishes.Add(fishName, 1);
        }
    }

    public void CameraMove()
    {
        //カメラ移動
        mainCamera.transform.position = player.transform.position + cameraBasePosition;
        //カメラ大きさ変更
    }

    public int NextExp
    {
        get
        {
            int result;
            float exeA = firstLevelUpSize;
            for (int n = 0; n < CurrentLevel - 1; ++n)
            {
                exeA *= multipleA;
            }
            float exeB = CurrentLevel * multipleB;
            result = (int)((exeA + exeB) / 2);

            return result;
        }
    }

    private void LevelUp()
    {
        if (0 <= currentExp - NextExp)
        {
            ++CurrentLevel;
            currentExp -= NextExp;
        }
    }

    public void Damage()
    {
        life -= damage * Time.deltaTime;
    }

    public void Eater(int level, string name)
    {
        currentExp += level * level;
        EatFishCounter(name);
    }

    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        eatFishTypes = new List<string>();
        eatFishes = new Dictionary<string, int>();
        CurrentLevel = 1;
        GameReset();
    }

    void Update()
    {
        CameraMove();
        LevelUp();
        time -= Time.deltaTime;
        UIUpdate();
        if (time <= 0)
        {

        }
    }

    public void UIUpdate()
    {
        timeText.text = $"Time:{Mathf.Floor(time)}";
        lifeText.text = $"HP:{Life}";
        levelText.text = $"NextEXP:{NextExp - currentExp}";
    }

    public void GameReset()
    {
        time = timeMax;
        life = maxLife;
        //UIバーを初期化
    }

    public void GameRecord()
    {
        GetTime = time;
        GetEatFishes = eatFishes;
        GetEatFishTypes = eatFishTypes;
    }
}
