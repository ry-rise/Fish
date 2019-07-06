using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField]
    protected BaseFish data = null;
    public BaseFish Data { get { return data; } }
    public int Level { get; protected set; }

    protected GameObject player;

    protected Rigidbody2D rb;

    protected SpriteRenderer sprite;
    [SerializeField]
    protected float popTime = 1;
    protected float popTimeCount = 0;
    public bool IsPoped { get; private set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Level = Random.Range(Data.MinLevel, Data.MaxLevel + 1);
        SizeChanger();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        IsPoped = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 50f)
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
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
    }

    abstract protected void Move();

    private void SizeChanger()
    {
        transform.localScale = new Vector2(1, 1) * (Level != 0 ? Level * 0.5f : 0.1f);
        Transform children = GetComponentInChildren<Transform>();
        foreach (Transform child in children)
        {
            child.localScale = Vector2.one / (Level != 0 ? Level * 0.5f : 0.1f);
        }
    }
}
