using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField]
    protected BaseFish data = null;
    public BaseFish Data { get { return data; } }

    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SizeChanger();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    abstract protected void Move();

    private void SizeChanger()
    {
        transform.localScale = new Vector2(1, 1) * data.Level;
    }
}
