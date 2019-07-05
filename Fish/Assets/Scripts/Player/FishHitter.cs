using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        Eater();
        Damage();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.transform.root.GetComponent<BaseEnemyAI>();
            if (!enemy.IsPoped) return;
            int fishALev = enemy.Data.Level;
            int fishBLev = gameManager.CurrentLevel;
            if (fishALev < fishBLev)
            {
                if (-1 == EatFish.IndexOf(enemy))
                {
                    EatFish.Add(enemy);
                }
            }
            else if (fishALev > fishBLev)
            {
                if (-1 == DamageFish.IndexOf(enemy))
                {
                    DamageFish.Add(enemy);
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.transform.root.GetComponent<BaseEnemyAI>();
            if (!enemy.IsPoped) return;
            if (EatFish.Any(a => a == enemy))
            {
                EatFish.Remove(enemy);
                return;
            }
            if (DamageFish.Any(a => a == enemy))
            {
                DamageFish.Remove(enemy);
                return;
            }
        }
    }

    private void Eater()
    {
        if (0 == EatFish.Count) return;
        for (int i = 0; i < EatFish.Count; ++i)
        {
            gameManager.Eater(EatFish[i].Data.Level, EatFish[i].name);
            Destroy(EatFish[i].gameObject);
        }
    }

    private void Damage()
    {
        if (0 == DamageFish.Count) return;
        for (int i = 0; i < DamageFish.Count; ++i)
        {
            gameManager.Damage();
        }
    }
}
