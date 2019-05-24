using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeControl : MonoBehaviour
{
    [SerializeField] private Text lifeText = null;
    [SerializeField] private float maxLife = 1;
    private float life;
    public float Life { get { return life; } }

    void Start()
    {
        GameReset();
    }

    void Update()
    {
        lifeText.text = $"HP:{Life}";
    }

    public void GameReset()
    {
        life = maxLife;
        //UIバーを初期化
    }
}
