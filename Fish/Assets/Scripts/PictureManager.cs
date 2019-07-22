using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using System.IO;
public class PictureManager : MonoBehaviour
{
    [SerializeField]
    private Text[] names = null;

    private string[] nameList = null;
    private int number;

    private List<string> typeList;
    private List<int> amountList;
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
            typeList = data.Type;
            amountList = data.Amount;
            number = 0;
            nameList = typeList.ToArray();
            NamesChanger();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextNames()
    {
        if (File.Exists(path))
        {
            number = (number + 1) % (int)Mathf.Ceil((float)nameList.Length / names.Length);
            NamesChanger();
        }
    }

    public void BackNames()
    {
        if (File.Exists(path))
        {
            number = (number + nameList.Length / names.Length - 1) % (int)Mathf.Ceil((float)nameList.Length / names.Length);
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

    public void BackToTitle()
    {
        EditorSceneManager.LoadScene("Title");
    }
}
