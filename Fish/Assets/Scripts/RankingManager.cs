using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
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

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

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

    public void BackToTitle()
    {
        EditorSceneManager.LoadScene("Title");
    }
}
