using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StokerTypeFish : BaseEnemyAI
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
        fs = transform.Find("FishSearcher").GetComponent<FishSearcher>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        Vector2 move = new Vector2(direction, 0) * data.Speed;
        if (fs.IsSmallTargeted)
        {
            Vector2 target = fs.SmallTargetPosition;
            Vector2 dir = target - (Vector2)transform.position;
            move = dir * data.Speed;
        }
        //正規化
        float size = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        Vector2 e = move / size;
        rb.velocity = e * data.Speed;
    }
}
