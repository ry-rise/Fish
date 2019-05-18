﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 1;
    private float life;
    public float Life { get { return life; } }

    // Start is called before the first frame update
    void Start()
    {
        GameReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameReset()
    {
        life = maxLife;
        //UIバーを初期化
    }
}
