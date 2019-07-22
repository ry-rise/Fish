﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : BaseEnemyAI
{
    private int direction;
    private FishSearcher fs;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameObject player = GameObject.Find("Player");
        float playerPositionX = player.transform.position.x;
        float myPositionX = transform.position.x;
        if (playerPositionX < myPositionX)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    public void SetFishSearcher(FishSearcher fs)
    {
        this.fs = fs;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        Vector2 move = new Vector2(direction, 0) * data.Speed;
        if (fs != null)
        {
            if (fs.IsLargeTargeted)
            {
                Debug.Log("やばい");
                Vector2 target = fs.LargeTargetPosition;
                Vector2 dir = (Vector2)transform.position - target;
                move = dir * data.Speed;
            }
            else if (fs.IsSmallTargeted)
            {
                Debug.Log("見つけた");
                Vector2 target = fs.SmallTargetPosition;
                Vector2 dir = target - (Vector2)transform.position;
                move = dir * data.Speed;
            }
        }
        //正規化
        float size = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (size != 0)
        {
            Vector2 e = move / size;
            rb.velocity = e * data.Speed;
        }
    }
}