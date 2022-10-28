using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float spawnPosY = 0;
    private float spawnRangeX = 15;
    private float spawnRangeZ = 120;

    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    public GameObject[] enemyPrefabs;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnRandomEnemy();
        }
    }

    public void startSpawn() {
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }
    void SpawnRandomEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnRangeZ);
        int animalIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[animalIndex], spawnPos, enemyPrefabs[animalIndex].transform.rotation);
    }
}
