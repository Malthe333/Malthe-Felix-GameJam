using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int waves = 3;
    public int enemiesPerWave = 3;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnZ = -2f;
    public Vector2 bossSpawnPosition;
    public float spawnRadius = 9f;
    public UnityEvent onBossKilled;

    private int waveCounter = 0;
    private GameObject[] enemyGameObjects;
    private bool bossSpawned = false;

    private enum WaveType
    {
        Normal,
        Boss
    }
    
    void Awake()
    {
        waveCounter = 0;
        enemyGameObjects = new GameObject[enemiesPerWave];
    }

    void Update()
    {
        int enemyCount = CountEnemies();
        Debug.Log($"Enemy Count: {enemyCount}");
        if (enemyCount == 0)
        {
            if (waveCounter < waves)
            {
                waveCounter++;
                SpawnNewWave(WaveType.Normal);
            }
            else if (!bossSpawned)
            {
                bossSpawned = true;
                SpawnNewWave(WaveType.Boss);
            }
            else
            {
                onBossKilled?.Invoke();
            }
        }
    }

    int CountEnemies()
    {
        int count = 0;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (enemyGameObjects[i])
            {
                count++;
            }
        }
        return count;
    }

    void SpawnNewWave(WaveType waveType)
    {
        if (waveType == WaveType.Normal) {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
                enemyGameObjects[i] = enemy;
            }
        }
        else if (waveType == WaveType.Boss)
        {
            Vector3 bossPosition = new Vector3(bossSpawnPosition.x, bossSpawnPosition.y, spawnZ);
            GameObject boss = Instantiate(bossPrefab, bossPosition, Quaternion.identity);
            enemyGameObjects[0] = boss;
        }
    }

    Vector3 RandomSpawnPosition()
    {
        float angle = Random.Range(0, 2 * Mathf.PI); // Radians
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 position = direction * spawnRadius;
        return new Vector3(position.x, position.y, spawnZ);
    }
}
