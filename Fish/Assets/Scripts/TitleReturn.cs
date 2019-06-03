using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleReturn : MonoBehaviour
{
    public void TitleStart()
    {
        SceneManager.LoadScene("Title");
    }
}
