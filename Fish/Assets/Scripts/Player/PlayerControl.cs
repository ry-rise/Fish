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
    [SerializeField]
    private SpriteRenderer sprite = null;
    private GameManager manager;
    public int Level { get { return manager.CurrentLevel; } }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SizeChanger();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.State == GameManager.GameStatus.Play)
        {
            SizeChanger();
            MainControl();
        }
    }

    private void MainControl()
    {
        //操作
        float getX = Input.GetAxis("Horizontal");
        float getY = Input.GetAxis("Vertical");
        if (getX != 0 || getY != 0)
        {
            if (getX < 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
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

    public void SizeChanger()
    {
        //ゲームマネージャーからレベルを見る
        //Vector2 size = transform.localScale;
        transform.localScale = new Vector2(1, 1) * manager.CurrentLevel * 0.5f;
    }
}