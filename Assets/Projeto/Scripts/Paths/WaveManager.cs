using UnityEngine;
using System.Collections;


public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    public EnemyWave[] waves;

    int currentWave = 0;

    bool spawning = false;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartWave();
    }
    public void StartWave()
    {
        if (spawning)
            return;

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        spawning = true;

        EnemyWave wave = waves[currentWave];

        foreach (EnemyGroup group in wave.groups)
        {
            for (int i = 0; i < group.count; i++)
            {
                SpawnEnemy(group.enemyPrefab);

                yield return new WaitForSeconds(group.spawnDelay);
            }
        }

        spawning = false;
        currentWave++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPos = EnemyPath.instance.GetWaypoint(0).position;
        GameObject enemy = ObjectPool.instance.GetObject(enemyPrefab, spawnPos);

        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        if (eh != null)
            eh.SetPrefabReference(enemyPrefab);
    }
}