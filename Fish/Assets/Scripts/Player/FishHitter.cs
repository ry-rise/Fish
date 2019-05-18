using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHitter : MonoBehaviour
{
    public List<GameObject> Fish { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Fish = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            if (-1 == Fish.IndexOf(collision.gameObject))
            {
                Fish.Add(collision.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            if (-1 != Fish.IndexOf(collision.gameObject))
            {
                Fish.Remove(collision.gameObject);
            }
        }
    }
}
