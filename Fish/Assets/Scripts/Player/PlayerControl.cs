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
    private GameManager manager;
    public int Level { get { return manager.CurrentLevel; } }
    [SerializeField]
    private GameObject upWall = null;
    [SerializeField]
    private GameObject downWall = null;
    [SerializeField]
    private GameObject rightWall = null;
    [SerializeField]
    private GameObject leftWall = null;

    public enum HitDirection
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Right = 1 << 2,
        Left = 1 << 3
    }
    private int hitWall = (int)HitDirection.None;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //X
        if (collider.gameObject == rightWall && (hitWall & (int)HitDirection.Right) != (int)HitDirection.Right)
        {
            hitWall += (int)HitDirection.Right;
        }
        else if (collider.gameObject == leftWall && (hitWall & (int)HitDirection.Left) != (int)HitDirection.Left)
        {
            hitWall += (int)HitDirection.Left;
        }
        //Y
        else if (collider.gameObject == upWall && (hitWall & (int)HitDirection.Up) != (int)HitDirection.Up)
        {
            hitWall += (int)HitDirection.Up;
        }
        else if (collider.gameObject == downWall && (hitWall & (int)HitDirection.Down) != (int)HitDirection.Down)
        {
            hitWall += (int)HitDirection.Down;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        //X
        if (collider.gameObject == rightWall && (hitWall & (int)HitDirection.Right) == (int)HitDirection.Right)
        {
            hitWall -= (int)HitDirection.Right;
        }
        else if (collider.gameObject == leftWall && (hitWall & (int)HitDirection.Left) == (int)HitDirection.Left)
        {
            hitWall -= (int)HitDirection.Left;
        }
        //Y
        else if (collider.gameObject == upWall && (hitWall & (int)HitDirection.Up) == (int)HitDirection.Up)
        {
            hitWall -= (int)HitDirection.Up;
        }
        else if (collider.gameObject == downWall && (hitWall & (int)HitDirection.Down) == (int)HitDirection.Down)
        {
            hitWall -= (int)HitDirection.Down;
        }
    }

    private bool WallHitter(HitDirection direction)
    {
        return (hitWall & (int)direction) == (int)direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.State == GameManager.GameStatus.Play)
        {
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
                transform.localScale = new Vector2(1, 1);
            }
            else if (getX > 0)
            {
                transform.localScale = new Vector2(-1, 1);
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

        if (WallHitter(HitDirection.Right))
        {
            rb.velocity = new Vector2(-1, rb.velocity.y);
        }
        else if (WallHitter(HitDirection.Left))
        {
            rb.velocity = new Vector2(1, rb.velocity.y);
        }
        if (WallHitter(HitDirection.Up))
        {
            rb.velocity = new Vector2(rb.velocity.x, -1);
        }
        else if (WallHitter(HitDirection.Down))
        {
            rb.velocity = new Vector2(rb.velocity.x, 1);
        }
    }
}