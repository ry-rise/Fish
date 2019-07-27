using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.SceneManagement;
public class GameManager : MonoBehaviour
{
    //時間
    [SerializeField]
    private Slider timeSlider = null;
    [SerializeField]
    private float timeMax = 100;
    private float time = 0;
    //ライフ
    [SerializeField]
    private Slider lifeSlider = null;

    [SerializeField]
    private float maxLife = 1;
    public float Life { get; private set; }
    [SerializeField]
    private float damage = 1;

    //レベル＆サイズ
    [SerializeField]
    private Slider expSlider = null;
    [SerializeField]
    private float firstLevelUpSize = 10f;
    [SerializeField]
    private float multipleA = 1.1f;
    [SerializeField]
    private float multipleB = 15;
    private float currentExp;
    public int CurrentLevel { get; private set; }

    private PlayerControl player;
    private Camera mainCamera;

    [SerializeField]
    private Vector3 cameraBasePosition = Vector3.zero;

    private List<string> eatFishTypes;
    private Dictionary<string, int> eatFishes;

    public static float GetTime = 0;//時間
    public static List<string> GetEatFishTypes = new List<string>(); //食べた魚の名前
    public static Dictionary<string, int> GetEatFishes = new Dictionary<string, int>();//食べた魚の数

    public static bool GameClear = false;
    public static int LastLevel = 1;

    [SerializeField]
    private int ClearLevel = 5;

    public enum GameStatus
    {
        Start,
        Play,
        End
    }
    public GameStatus State { get; private set; }
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
            currentExp -= NextExp;
            ++CurrentLevel;
        }
    }

    public void Damage()
    {
        Life -= damage * Time.deltaTime;
    }

    public void Eater(int levelGap, string name)
    {
        float value = CurrentLevel + Mathf.Pow(2, levelGap);
        currentExp += value;
        EatFishCounter(name);
    }

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        eatFishTypes = new List<string>();
        eatFishes = new Dictionary<string, int>();
        CurrentLevel = 1;
        State = GameStatus.Start;
        GameReset();
    }

    void Update()
    {
        switch (State)
        {
            case GameStatus.Start:
                StartGame();
                break;
            case GameStatus.Play:
                PlayGame();
                break;
            case GameStatus.End:
                EndGame();
                break;
        }
    }

    private void StartGame()
    {
        CameraMove();
        UIUpdate();
        State = GameStatus.Play;
    }
    private void PlayGame()
    {
        CameraMove();
        LevelUp();
        time -= Time.deltaTime;
        UIUpdate();
        if (time <= 0)
        {
            State = GameStatus.End;
        }
        else if (Life <= 0)
        {
            State = GameStatus.End;
        }
        else if (ClearLevel <= CurrentLevel)
        {
            State = GameStatus.End;
        }
        if (Life < maxLife)
        {
            Life += Time.deltaTime * 0.1f;
        }
        else if (Life > maxLife)
        {
            Life = maxLife;
        }
    }
    private void EndGame()
    {
        GameRecord();
        EditorSceneManager.LoadScene("Result");   
    }

    public void UIUpdate()
    {
        timeSlider.value = time / timeMax;
        lifeSlider.value = Life / maxLife;
        expSlider.value = currentExp / NextExp;
    }

    public void GameReset()
    {
        time = timeMax;
        Life = maxLife;
        //UIバーを初期化
    }

    public void GameRecord()
    {
        GetTime = time;
        GetEatFishes = eatFishes;
        GetEatFishTypes = eatFishTypes;
        LastLevel = CurrentLevel;
        if (ClearLevel <= CurrentLevel)
        {
            GameClear = true;
        }
        else
        {
            GameClear = false;
        }
    }
}
