using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies=null;
    private GameObject[,] tables;
    private float timer;
    private float spawnTime = 5.0f;

    private void Start()
    {
        tables = new GameObject[4, 3];
        int enemiesNumber = 0;
        for (int x = 0; x < 4; x += 1)
        {
            for (int y = 0; y < 3; y += 1)
            {
                if (enemies[enemiesNumber] == null) { return; }
                tables[x, y] = enemies[enemiesNumber];
                enemiesNumber += 1;
            }
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        if (timer >= spawnTime)
        {
            if (enemies != null)
            {
                Instantiate(tables[3, 1], new Vector3(5, 20, -10), Quaternion.identity);
                timer = 0;
            }
        }
    }
}
