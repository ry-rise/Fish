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
}
