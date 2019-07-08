using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private Button result;
    
    public void Go()
    {
        SceneManager.LoadScene("Title");
    }

    // Start is called before the first frame update
    void Start()
    {
        List<float> times = new List<float>();
        times.Add(GameManager.GetTime);
        for (int i = 0; i < 5; ++i)
        {
            times.Add(SaveAndLoad.LoadData<float>($"{Application.persistentDataPath}\\Rank{i + 1}.json"));
        }

        times.Sort((a, b) => CompareByID(a, b));
        //記録
        for (int i = 0; i < 5; ++i)
        {
            SaveAndLoad.SaveData(times[i], $"{Application.persistentDataPath}\\Rank{i + 1}.json");
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
