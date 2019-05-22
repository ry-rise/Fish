using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideInTypeFish : BaseEnemyAI
{
    private int direction;

    // Start is called before the first frame update
    override protected void Start()
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

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        rb.velocity = new Vector2(direction, 0) * data.Speed;
        //正規化
        if (rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y > data.Speed * data.Speed)
        {
            float size = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
            Vector2 e = rb.velocity / size;
            rb.velocity = e * data.Speed;
        }
    }
}
