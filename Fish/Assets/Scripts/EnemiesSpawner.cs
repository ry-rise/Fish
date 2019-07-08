using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private float playerRange = 1.0f;
    [SerializeField] private float spawnTime = 5.0f;
    [SerializeField] private Group[] groups = null;
    private float timer;
    private GameManager manager;
    private GameObject player;

    [System.Serializable]
    public class Group
    {
        [SerializeField]
        private GameObject[] enemies = null;
        public GameObject[] Enemies { get { return enemies; } }
    }

    private void Start()
    {
        manager = GetComponent<GameManager>();
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (manager.State == GameManager.GameStatus.Play)
        {
            timer += Time.deltaTime;
            EnemySpawn();
        }
    }

    private void EnemySpawn()
    {
        if (groups.Length > manager.CurrentLevel - 1)
        {
            if (timer >= spawnTime)
            {
                int tableNum = manager.CurrentLevel - 1;
                float r = Mathf.Sqrt(Random.Range(0.0f, 1.0f)) * playerRange;
                float angle = Random.rotation.y * Mathf.Rad2Deg;
                Vector2 position = new Vector2(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);

                GameObject prefab = groups[tableNum].Enemies[Random.Range(0, groups[tableNum].Enemies.Length)];
                Instantiate(prefab, (Vector2)player.transform.position + position, Quaternion.identity);
                timer = 0;
            }
        }
    }
}