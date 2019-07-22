using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class ResultManager : MonoBehaviour
{
    private int saveListAmount = 5;
    [SerializeField]
    private SaveAndLoad sal = null;

    public void Go()
    {
        SceneManager.LoadScene("Title");
    }

    // Start is called before the first frame update
    void Start()
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
            List<float> times = new List<float>();
            times.Add(GameManager.GetTime);
            for (int i = 0; i < data.Time.Count; ++i)
            {
                times.Add(data.Time[i]);
            }

            times.Sort((a, b) => CompareByID(a, b));
            //記録
            for (int i = 0; i < (times.Count < saveListAmount ? times.Count : saveListAmount); ++i)
            {
                data.Time.Add(times[i]);
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

    // Update is called once per frame
    void Update()
    {

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
}
