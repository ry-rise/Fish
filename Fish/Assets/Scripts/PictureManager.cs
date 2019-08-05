using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class PictureManager : MonoBehaviour
{
    [SerializeField]
    private Text[] names = null;

    private string[] nameList = null;
    private int number;
    
    private List<int> amountList;
    private string path;
    [SerializeField]
    private SaveAndLoad sal = null;
    [SerializeField]
    private Button backToTitle=null;
    [SerializeField]
    private Button backButton=null;
    [SerializeField]
    private Button nextButton=null;
    [SerializeField]
    private AudioSource pushSound=null;

    private void Awake()
    {
        backToTitle.onClick.AddListener(BackToTitle);
        backButton.onClick.AddListener(BackNames);
        nextButton.onClick.AddListener(NextNames);
    }
    private void Start()
    {
        path = $"{Application.persistentDataPath}\\saveData.json";
        if (File.Exists(path))
        {
            Data data = sal.LoadData(path);
            amountList = data.Amount;
            number = 0;
            nameList = data.Type.ToArray();
            NamesChanger();
        }
    }

    private void NextNames()
    {
        pushSound.Play();
        if (File.Exists(path))
        {
            number = (number + 1) % (int)Mathf.Ceil((float)nameList.Length / names.Length);
            NamesChanger();
        }
    }

    private void BackNames()
    {
        pushSound.Play();
        if (File.Exists(path))
        {
            number = (number + (int)Mathf.Ceil((float)nameList.Length / names.Length) - 1) % (int)Mathf.Ceil((float)nameList.Length / names.Length);
            Debug.Log(number);
            NamesChanger();
        }
    }

    private void NamesChanger()
    {
        for (int i = 0; i < names.Length; ++i)
        {
            if (nameList == null || number * names.Length + i >= nameList.Length)
            {
                names[i].text = " ";
            }
            else
            {
                names[i].text = nameList[number * names.Length + i];
            }
        }
    }

    private void BackToTitle()
    {
        pushSound.Play();
        SceneManager.LoadScene("Title");
    }
}
