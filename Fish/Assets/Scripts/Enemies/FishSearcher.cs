using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FishSearcher : MonoBehaviour
{
    private List<BaseEnemyAI> mobFish;
    private PlayerControl playerFish;
    private BaseEnemyAI parent;

    // Start is called before the first frame update
    void Start()
    {
        //parent = transform.parent.GetComponent<BaseEnemyAI>();
        mobFish = new List<BaseEnemyAI>();
        playerFish = null;
    }

    public void SetFish(GameObject fish)
    {
        parent = fish.GetComponent<BaseEnemyAI>();
        ((FishAI)parent).SetFishSearcher(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            mobFish.RemoveAll(a => a == null);
            transform.position = parent.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Vector2 LargeTargetPosition
    {
        get
        {
            Vector2 top = Vector2.zero;
            float dis = float.MaxValue;
            if (null != playerFish && parent.Level < playerFish.Level)
            {
                top = playerFish.transform.position;
                dis = Vector2.Distance(transform.position, playerFish.transform.position);
            }
            foreach (BaseEnemyAI it in mobFish)
            {
                if (it == null) continue;
                if (!it.IsPoped) continue;
                if (parent.Level > it.Level) continue;
                float cDis = Vector2.Distance(transform.position, it.transform.position);
                if(dis > cDis)
                {
                    top = it.transform.position;
                    dis = cDis;
                }
            }
            return top;
        }
    }

    public Vector2 SmallTargetPosition
    {
        get
        {
            Vector2 top = Vector2.zero;
            float dis = float.MaxValue;
            if (null != playerFish && parent.Level > playerFish.Level)
            {
                top = playerFish.transform.position;
                dis = Vector2.Distance(transform.position, playerFish.transform.position);
            }
            foreach (BaseEnemyAI it in mobFish)
            {
                if (it == null) continue;
                if (!it.IsPoped) continue;
                if (parent.Level < it.Level) continue;
                float cDis = Vector2.Distance(transform.position, it.transform.position);
                if (dis > cDis)
                {
                    top = it.transform.position;
                    dis = cDis;
                }
            }
            return top;
        }
    }

    public bool IsLargeTargeted
    {
        get
        {
            if (null != playerFish && parent.Level < playerFish.Level)
            {
                return true;
            }
            if (0 != mobFish.Count)
            {
                foreach (BaseEnemyAI it in mobFish)
                {
                    if (parent.Level < it.Level) return true;
                }
            }
            return false;
        }
    }
    public bool IsSmallTargeted
    {
        get
        {
            if (null != playerFish && parent.Level > playerFish.Level)
            {
                return true;
            }
            if (0 != mobFish.Count)
            {
                foreach (BaseEnemyAI it in mobFish)
                {
                    if (parent.Level > it.Level) return true;
                }
            }
            return false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.transform.root.GetComponent<BaseEnemyAI>();
            mobFish.Add(enemy);
        }
        else if (collision.tag == "Player")
        {
            PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
            playerFish = player;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            BaseEnemyAI enemy = collision.transform.root.GetComponent<BaseEnemyAI>();
            mobFish.Remove(enemy);
        }
        else if (collision.gameObject.tag == "Player")
        {
            playerFish = null;
        }
    }
}
