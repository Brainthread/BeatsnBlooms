using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager current;
    private int beatsTillNextEnemy = 3;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemySpawnLongitude = 15;
    private float interpolationValue;
    private EnemySpawn[] enemySpawns;
    private int enemySpawnIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(EnemySpawn[] enemySpawns)
    {
        current = this;
        this.enemySpawns = enemySpawns;
        enemySpawnIndex = 0;
        EventHandler.current.onBeat += onNewBeat;
        EventHandler.current.onStartSong += OnStartSong;
    }

    public void OnFrameBeatTime(float value)
    {
        interpolationValue = value;
    }
    public float GetInterpolationValue()
    {
        return interpolationValue;
    }

    void OnStartSong()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void onNewBeat()
    {
        int currentBeat = (int)Mathf.Floor(MusicManager.instance.GetBeat());
        for (int i = 0; i < enemySpawns.Length; i++)
        {
            EnemySpawn enemySpawn = enemySpawns[i];
            if(enemySpawn.beat == currentBeat)
            { 
                if (enemySpawn != null) {
                    Vector3 spawnPosition = GridManager.current.GridPositionToWorldPosition(enemySpawn.spawnPosition);
                    SpawnEnemy(enemySpawn.enemy, spawnPosition);
                }
                enemySpawnIndex += 1;
            }
        }
    }

    void SpawnEnemy(GameObject enemy, Vector3 spawnPosition)
    {
        GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.Euler(new Vector3(180, 90, 0)));
        spawnedEnemy.transform.parent = this.transform;
    }
}
