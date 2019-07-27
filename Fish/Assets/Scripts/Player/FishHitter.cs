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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.transform.root.GetComponent<BaseEnemyAI>();
            int fishALev = enemy.LevelGap;
            if (fishALev < 0)
            {
                if (-1 == EatFish.IndexOf(enemy))
                {
                    EatFish.Add(enemy);
                }
            }
            else if (fishALev > 0)
            {
                if (-1 == DamageFish.IndexOf(enemy))
                {
                    DamageFish.Add(enemy);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.GetComponent<BaseEnemyAI>();
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
            if (!EatFish[i].IsPoped) continue;
            gameManager.Eater(EatFish[i].LevelGap, EatFish[i].Data.FishName);
            Destroy(EatFish[i].gameObject);
        }
    }

    private void Damage()
    {
        if (0 == DamageFish.Count) return;
        for (int i = 0; i < DamageFish.Count; ++i)
        {
            if (!DamageFish[i].IsPoped) continue;
            gameManager.Damage();
        }
    }
}
