using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb = null;
    [SerializeField]
    private float maxSpeed = 0;
    [SerializeField]
    private float addSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MainControl();
    }

    private void MainControl()
    {
        //操作
        float getX = Input.GetAxis("Horizontal");
        float getY = Input.GetAxis("Vertical");
        if (getX != 0 || getY != 0)
        {
            rb.velocity += new Vector2(getX, getY) * addSpeed;
            //正規化
            if (rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y > maxSpeed * maxSpeed)
            {
                float size = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
                Vector2 e = rb.velocity / size;
                rb.velocity = e * maxSpeed;
            }
        }
        else
        {
            rb.velocity *= 0.95f;
        }
    }

    private void SizeChanger()
    {
        //ゲームマネージャーからレベルを見る
        Vector2 size = transform.localScale;
    }
}
