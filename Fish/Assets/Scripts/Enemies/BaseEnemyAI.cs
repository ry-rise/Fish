using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField]
    protected BaseFish data = null;
    public BaseFish Data { get { return data; } }
    public int LevelGap { get; protected set; }

    private Vector2 scale;

    protected PlayerControl player;

    protected Rigidbody2D rb;

    protected SpriteRenderer sprite;
    [SerializeField]
    protected float popTime = 1;
    protected float popTimeCount = 0;
    public bool IsPoped { get; private set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        LevelGap = Random.Range(Data.MinLevel, Data.MaxLevel + 1) - player.Level;
        SizeChanger();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        IsPoped = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 35f)
        {
            Destroy(gameObject);
        }
        if (!IsPoped)
        {
            popTimeCount += Time.deltaTime;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, popTimeCount / popTime);
            if (popTimeCount >= popTime)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
                IsPoped = true;
            }
        }
        else
        {
            Move();
            if (rb.velocity.x < 0)
            {
                transform.localScale = scale;
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = scale * new Vector2(-1, 1);
            }
        }
    }

    abstract protected void Move();

    private void SizeChanger()
    {
        scale = Vector2.one * Mathf.Pow(1.25f, LevelGap);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPoped && collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.GetComponent<BaseEnemyAI>();
            if (enemy.IsPoped)
            {
                int fishALev = enemy.LevelGap;
                int fishBLev = LevelGap;
                if (fishALev < fishBLev)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
