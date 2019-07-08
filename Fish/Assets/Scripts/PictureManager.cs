using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class PictureManager : MonoBehaviour
{
    [SerializeField]
    private Text[] names = null;

    private string[] nameList = null;
    private int number;

    // Start is called before the first frame update
    void Start()
    {
        number = 0;
        if (GameManager.GetEatFishTypes != null)
        {
            nameList = GameManager.GetEatFishTypes.ToArray();
        }
        NamesChanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextNames()
    {
        if (GameManager.GetEatFishTypes != null)
        {
            number = (number + 1) % nameList.Length / (names.Length - 1);
        }
        NamesChanger();
    }

    public void BackNames()
    {
        if (GameManager.GetEatFishTypes != null)
        {
            number = (number + nameList.Length / names.Length - 1) % nameList.Length / names.Length - 1;
        }
        NamesChanger();
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
