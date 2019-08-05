using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;


public class ResultManager : MonoBehaviour
{
    private int saveListAmount = 5;
    [SerializeField]
    private SaveAndLoad sal = null;
    [SerializeField]
    private Text resultText = null;
    [SerializeField]
    private InputField inputNameText = null;
    [SerializeField]
    private Text nameText = null;
    [SerializeField]
    private GameObject namePanel = null;
    private List<ScoreData> score = new List<ScoreData>();
    private ScoreData inputScore = null;  //-1の時は入力無し

    public void Go()
    {
        if (GameManager.GameClear && inputScore != null)
        {
            namePanel.SetActive(true);
        }
        else
        {
            //記録保存
            GameResultSave();
            SceneManager.LoadScene("Title");
        }
    }

    public void NameDecision()
    {
        if (nameText.text != "")
        {
            inputScore.name = nameText.text;
            //記録保存
            GameResultSave();
            SceneManager.LoadScene("Title");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //結果表示
        //クリアしたなら、時間
        if (GameManager.GameClear)
        {
            resultText.text = "よくできました\n時間『" + GameManager.GetTime + "』";
        }
        //そうでないなら、レベルを表示する
        else
        {
            resultText.text = "もう少し頑張りましょう";
        }
        inputScore = MyTimeRecord();
        //食べた魚の数を表示する
    }
    public class ScoreData
    {
        public float time { get; set; }
        public string name { get; set; }
    }

    private ScoreData MyTimeRecord()
    {
        string path = $"{Application.persistentDataPath}\\saveData.json";

        Data data = null;
        if (File.Exists(path))
        {
            data = sal.LoadData(path);
        }
        if (!File.Exists(path) || data == null || data.Type == null || data.Amount == null)
        {
            data = new Data();
        }
        if (GameManager.GameClear)
        {
            score = new List<ScoreData>();
            score.Add(new ScoreData());
            score[0].time = GameManager.GetTime;
            score[0].name = "";
            for (int i = 0; i < data.Time.Count; ++i)
            {
                score.Add(new ScoreData());
                score[score.Count - 1].time = data.Time[i];
                score[score.Count - 1].name = data.Name[i];
            }

            score.Sort((a, b) => CompareByID(a.time, b.time));
            ScoreData myScore = score.Find(a => a.name == "");
            if (score.Count < saveListAmount && score[score.Count -1] == myScore)
            {
                return null;
            }
            return myScore;
        }
        return null;
    }
    
    private void GameResultSave()
    {
        string path = $"{Application.persistentDataPath}\\saveData.json";

        Data data = null;
        if (File.Exists(path))
        {
            data = sal.LoadData(path);
        }
        if (!File.Exists(path) || data == null || data.Type == null || data.Amount == null)
        {
            data = new Data();
        }
        if (GameManager.GameClear)
        {
            //記録
            data.Time.Clear();
            data.Name.Clear();
            for (int i = 0; i < (score.Count < saveListAmount ? score.Count : saveListAmount); ++i)
            {
                data.Time.Add(score[i].time);
                data.Name.Add(score[i].name);
            }
        }
        //取得した魚の記録

        if (!File.Exists(path) || data == null || data.Type == null || data.Amount == null)
        {
            List<int> newEatAmount = new List<int>();

            for (int i = 0; i < GameManager.GetEatFishTypes.Count; ++i)
            {
                newEatAmount.Add(GameManager.GetEatFishes[GameManager.GetEatFishTypes[i]]);
            }
            data.Type.AddRange(GameManager.GetEatFishTypes.ToArray());
            data.Amount.AddRange(newEatAmount.ToArray());
            sal.SaveData(data, path);
        }
        else
        {
            for (int i = 0; i < GameManager.GetEatFishTypes.Count; ++i)
            {
                int num = data.Type.IndexOf(GameManager.GetEatFishTypes[i]);
                if (num != -1)
                {
                    data.Amount[num] += GameManager.GetEatFishes[GameManager.GetEatFishTypes[i]];
                }
                else
                {
                    data.Type.Add(GameManager.GetEatFishTypes[i]);
                    data.Amount.Add(GameManager.GetEatFishes[GameManager.GetEatFishTypes[i]]);
                }
            }
            sal.SaveData(data, path);
        }
    }
    private static int CompareByID(float a, float b)
    {
        if (a < b)
        {
            return -1;
        }

        if (a > b)
        {
            return 1;
        }

        return 0;
    }

    public void InputName()
    {
        nameText.text = inputNameText.text;
    }
}
