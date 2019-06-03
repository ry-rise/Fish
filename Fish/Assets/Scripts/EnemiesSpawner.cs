using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies=null;
    private float spawnTime;

    void Update()
    {
        spawnTime += Time.deltaTime;
        EnemySpawn();
    }

    private void EnemySpawn()
    {
        if (spawnTime >= 1.0f)
        {
            if (enemies != null)
            {
                Instantiate(enemies[Random.Range(0, enemies.Length)], transform);
                spawnTime = 0;
            }
        }
    }
}
