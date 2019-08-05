using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class RankingManager : MonoBehaviour
{
    [SerializeField]
    private Text[] scoreText = null;

    [SerializeField]
    private Text[] nameText = null;//

    private List<float> timeList ;

    private List<string> nameList; //

    private string path;
    [SerializeField]
    private SaveAndLoad sal = null;
    [SerializeField]
    private Button backToTitle=null;
    [SerializeField]
    private AudioSource pushButton=null;

    private void Awake()
    {
        backToTitle.onClick.AddListener(BackToTitle);

    }
    private void Start()
    {
        path = $"{Application.persistentDataPath}\\saveData.json";
        if (File.Exists(path))
        {
            Data data = sal.LoadData(path);
            timeList = data.Time;
            nameList = data.Name;
            NamesChanger();
        }
    }

    private void NamesChanger()
    {
        for (int i = 0; i < scoreText.Length; ++i)
        {
            if (i >= timeList.Count) break;
            scoreText[i].text = timeList[i].ToString();
        }

        for (int i = 0; i < nameText.Length; ++i) //
        {
            if (i >= nameList.Count) break;
            nameText[i].text = nameList[i].ToString();
        }
    }

    private void BackToTitle()
    {
        pushButton.Play();
        SceneManager.LoadScene("Title");
    }
}
