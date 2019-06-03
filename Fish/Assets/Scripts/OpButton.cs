using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OpButton : MonoBehaviour
{
    public void MainGameStart()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void PictureBookStart()
    {
        SceneManager.LoadScene("PictureBook");
    }
    public void RankingStart()
    {
        SceneManager.LoadScene("Ranking");
    }
}
