using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //時間
    [SerializeField]
    private Text timeText = null;
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
    private int currentLevel = 1;
    public int CurrentLevel { get { return currentLevel; } }

    public int NextExp
    {
        get
        {
            int result = 1;
            float exeA = firstLevelUpSize;
            for (int n = 0; n < currentLevel - 1; ++n)
            {
                exeA *= multipleA;
            }
            float exeB = currentLevel * multipleB;
            result = (int)((exeA + exeB) / 2);

            return result;
        }
    }

    private void LevelUp()
    {
        if (0 <= currentExp - NextExp)
        {
            ++currentLevel;
            currentExp -= NextExp;
        }
    }

    public void Damage()
    {
        life -= damage * Time.deltaTime;
    }

    public void Eater(int level)
    {
        currentExp += level * level;
    }

    void Start()
    {
        GameReset();
    }

    void Update()
    {
        LevelUp();
        time += Time.deltaTime;
        UIUpdate();
    }

    public void UIUpdate()
    {
        timeText.text = $"Time:{Mathf.Floor(time)}";
        lifeText.text = $"HP:{Life}";
        levelText.text = $"NextEXP:{NextExp}";
    }

    public void GameReset()
    {
        time = 0;
        life = maxLife;
        //UIバーを初期化
    }
}
