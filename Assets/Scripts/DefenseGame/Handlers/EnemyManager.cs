using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager current;
    private int beatsTillNextEnemy = 3;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemySpawnLongitude = 15;
    private float interpolationValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = this;
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
    // Update is called once per frame
    void Update()
    {
        
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
        beatsTillNextEnemy -= 1;
        if(beatsTillNextEnemy <= 0)
        {
            beatsTillNextEnemy = Random.Range(0, 12);
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            int spawnHeight = Random.Range(0, GridManager.current.GetRows());
            Vector3 spawnPosition = GridManager.current.GridPositionToWorldPosition(new Vector2(enemySpawnLongitude, spawnHeight));
            GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.Euler(new Vector3(180, 90, 0)));
            spawnedEnemy.transform.parent = this.transform;
        }
    }
}
