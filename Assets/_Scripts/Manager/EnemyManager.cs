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
    public float spawnPosY = 0;
    float spawnRangeX = 15;
    float spawnRangeZ = 120;

    float startDelay = 2;
    float spawnInterval = 1.5f;
    [SerializeField]
    public Enemy _enemyPrefab;
    List<Enemy> enemyList;
    public EnemyEvents events;
    // Start is called before the first frame update
    void Start()
    {
        events = new EnemyEvents();
        enemyList = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     SpawnRandomEnemy();
        // }
    }

    public void StartSpawn()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }
    void SpawnRandomEnemy()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(
                -spawnRangeX,
                spawnRangeX
            ),
            0,
            spawnRangeZ
        );

        Enemy enemyObj = Instantiate(_enemyPrefab);
        enemyObj.transform.position = spawnPos;
        // _enemyPrefab.transform.rotation
        AddEnemy(enemyObj);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemy.enemyEventsRef = events;
        enemyList.Add(
            enemy
        );
    }

    public void DestroyEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy);
    }
}
