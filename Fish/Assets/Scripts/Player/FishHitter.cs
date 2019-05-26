using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishHitter : MonoBehaviour
{
    public List<BaseEnemyAI> EatFish { get; private set; }
    public List<BaseEnemyAI> DamageFish { get; private set; }
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        EatFish = new List<BaseEnemyAI>();
        DamageFish = new List<BaseEnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            int fishALev = collision.gameObject.GetComponent<BaseEnemyAI>().Data.Level;
            int fishBLev = gameManager.CurrentLevel;
            if (-1 == DamageFish.IndexOf(collision.gameObject.GetComponent<BaseEnemyAI>()))
            {
                if (fishALev > fishBLev)
                {
                    EatFish.Add(collision.gameObject.GetComponent<BaseEnemyAI>());
                }
                else if (fishALev < fishBLev)
                {
                    DamageFish.Add(collision.gameObject.GetComponent<BaseEnemyAI>());

                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            int fishALev = collision.gameObject.GetComponent<BaseEnemyAI>().Data.Level;
            int fishBLev = gameManager.CurrentLevel;
            if (-1 != DamageFish.IndexOf(collision.gameObject.GetComponent<BaseEnemyAI>()))
            {
                if (fishALev > fishBLev)
                {
                    EatFish.Add(collision.gameObject.GetComponent<BaseEnemyAI>());
                }
                else if (fishALev < fishBLev)
                {
                    DamageFish.Add(collision.gameObject.GetComponent<BaseEnemyAI>());

                }
            }
        }
    }

    private void Eater()
    {
        foreach (BaseEnemyAI it in EatFish)
        {
            gameManager.Eater(it.Data.Level);
            Destroy(it.gameObject);
        }
        EatFish.Clear();
    }

    private void Damage()
    {
        foreach (BaseEnemyAI it in DamageFish)
        {
            gameManager.Damage();
        }
    }
}
