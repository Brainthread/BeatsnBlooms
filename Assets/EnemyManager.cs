using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int beatsTillNextEnemy = 3;
    [SerializeField]private GameObject[] enemyPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onBeat += onNewBeat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onNewBeat()
    {
        beatsTillNextEnemy -= 1;
        if(beatsTillNextEnemy <= 0)
        {
            beatsTillNextEnemy = Random.Range(0, 12);
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            float[] laneHeights = LaneManager.current.GetLaneHeights();
            float height = laneHeights[Random.Range(0, laneHeights.Length)];
            Vector3 pos = new Vector3(87.24001f, height, 0);
            Instantiate(enemy, pos, Quaternion.Euler(new Vector3(180, 90, 0)));
        }
    }
}
