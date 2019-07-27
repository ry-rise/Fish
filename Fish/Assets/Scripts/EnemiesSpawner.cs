using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float playerRange = 1.0f;
    [SerializeField] private float spawnTime = 5.0f;
    [SerializeField] private int enemyLimitNumber = 1;
    [SerializeField] private GameObject searchPrefab = null;
    private Group[] groups = null;
    private float timer;
    private GameManager manager;
    private GameObject player;
    private List<GameObject> enemies = new List<GameObject>();

    [SerializeField] private GameObject[] fishPrefas = null;

    [System.Serializable]
    public class Group
    {
        public GameObject[] Enemies { get; private set; }

        public Group(GameObject[] enemies)
        {
            Enemies = enemies;
        }
    }

    private void Start()
    {
        manager = GetComponent<GameManager>();
        player = GameObject.Find("Player");
        FishLoad();
    }
    private void Update()
    {
        if (manager.State == GameManager.GameStatus.Play)
        {
            timer += Time.deltaTime;
            EnemySpawn();
        }
    }
    private void FishLoad()
    {
        //ロード
        TextAsset csv = Resources.Load<TextAsset>("FishMemo");
        StringReader reader = new StringReader(csv.text);
        List<string[]> csvDatas = new List<string[]>();
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        //マックスの最大数を見る
        int maxLvl = 0;
        csvDatas.Remove(csvDatas[0]);
        foreach (string[] it in csvDatas)
        {
            int ml = int.Parse(it[2]);
            if (ml > maxLvl)
            {
                maxLvl = ml;
            }
        }
        //配列の数を決める
        groups = new Group[maxLvl];
        //範囲内に含まれる魚をリストに入れる
        List<GameObject> fishs = new List<GameObject>();
        for (int i = 0; i < groups.Length; ++i)
        {
            fishs.Clear();
            foreach (string[] it in csvDatas)
            {
                int min = int.Parse(it[1]);
                int max = int.Parse(it[2]);
                if (min > i + 1 || i + 1 > max) continue;

                foreach (GameObject prefab in fishPrefas)
                {
                    if (prefab.name != it[0]) continue;
                    fishs.Add(prefab);
                    break;
                }
            }
            groups[i] = new Group(fishs.ToArray());
        }
    }
    private void EnemySpawn()
    {
        if (groups.Length <= manager.CurrentLevel - 1) return;
        if (timer < spawnTime) return;
        enemies.RemoveAll(a => a == null);
        if (enemies.Count >= enemyLimitNumber) return;

        int tableNum = manager.CurrentLevel - 1;
        float r = Mathf.Sqrt(Random.Range(0.0f, 1.0f)) * playerRange;
        float angle = Random.rotation.y * Mathf.Rad2Deg;
        Vector2 setPos = new Vector2(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);

        GameObject prefab = groups[tableNum].Enemies[Random.Range(0, groups[tableNum].Enemies.Length)];
        GameObject fish = Instantiate(prefab, (Vector2)player.transform.position + setPos, Quaternion.identity);
        GameObject search = Instantiate(searchPrefab, (Vector2)player.transform.position + setPos, Quaternion.identity);
        search.GetComponent<FishSearcher>().SetFish(fish);
        enemies.Add(fish);
        timer = 0;
    }
}