using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public enum EnemyEventTypes
{
    OutArea,
    Died
}
public class EnemyEvents : UnityEvent<EnemyEventTypes, Enemy> { };
public class EnemyManager : MonoBehaviour
{
    bool autoSpawnIsActive = false;

    float delayWave = 0f;
    float spawnInterval = 0f;
    float waveTime = 0f;
    float spawnXSpread = 0f;
    float spawnXWave = 0;
    float nextSpawnTime = 0f;
    float endWaveTime = 0f;

    float spawnRangeX = 15;
    public float spawnPosY = 0;
    float spawnRangeZ = 120;
    float minSpawnInterval = 0.2f;

    [SerializeField]
    public Enemy _prefab;
    List<Enemy> enemyList;
    public EnemyEvents events;

    public void ResetSpawn()
    {
        delayWave = 15f;
        spawnInterval = 2f;
        waveTime = 3f;
        spawnXSpread = 0.5f;
        spawnXWave = 0;
        nextSpawnTime = 0f;
        endWaveTime = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        events = new EnemyEvents();
        enemyList = new List<Enemy>();
        ResetSpawn();
    }
    void Update()
    {
        if (autoSpawnIsActive)
        {
            if (Time.fixedTime >= endWaveTime) // next wave spawn
            {
                NextSpawnWave();
            }
            else if (Time.fixedTime >= nextSpawnTime) // next spawn
            {
                SpawnRandomEnemy();
                NextSpawnSpawn();
            }
        }
    }
    public void StartAutoSpawn()
    {
        autoSpawnIsActive = true;
        NextSpawnWave();
    }
    public void StopAutoSpawn()
    {
        autoSpawnIsActive = false;
    }
    void NextSpawnSpawn()
    {
        nextSpawnTime = Time.fixedTime + spawnInterval;
    }
    void NextSpawnWave()
    {
        delayWave -= 0.5f;
        spawnInterval -= 0.3f;
        if (spawnInterval < minSpawnInterval)
        {
            spawnInterval = minSpawnInterval;
        }
        waveTime += 1f;
        spawnXSpread += 0.5f;
        spawnXWave = Random.Range(
                -spawnRangeX + spawnXSpread,
                spawnRangeX - spawnXSpread
        );
        nextSpawnTime = Time.fixedTime + delayWave + spawnInterval;
        endWaveTime = nextSpawnTime + waveTime;
    }
    void SpawnRandomEnemy()
    {
        float randomX = Random.Range(
                -(spawnXWave + spawnXSpread),
                spawnXWave + spawnXSpread
            );
        if (randomX < -spawnRangeX)
        {
            randomX = -spawnRangeX;
        }
        if (randomX > spawnRangeX)
        {
            randomX = spawnRangeX;
        }
        Vector3 spawnPos = new Vector3(
            randomX,
            0,
            spawnRangeZ
        );
        Enemy enemyObj = Instantiate(_prefab);
        enemyObj.transform.position = spawnPos;
        AddEnemy(enemyObj);
    }
    public void AddEnemy(Enemy enemy)
    {
        enemy.enemyEventsRef = events;
        enemyList.Add(
            enemy
        );
    }
    public void DestroyAllEnemy()//TODO fix remove all enemies
    {
        foreach (Enemy enemy in enemyList)
        {
            enemy.DestroyObjectDelayed();
        }
        enemyList = new List<Enemy>();
    }
    public void DestroyEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy);
    }
}
