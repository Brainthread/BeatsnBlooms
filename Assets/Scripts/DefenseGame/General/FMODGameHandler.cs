using UnityEngine;

public class FMODGameHandler : MonoBehaviour
{
     private MusicManager musicManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GrowthManager growthManager;
    [SerializeField] private Stage stage;


    void Start()
    {
        Invoke("InitializeGame", 0.5f);
    }

    void InitializeGame()
    {
        enemyManager.Initialize(stage.enemySpawns);
        //musicManager.Initialize(stage.song);
        //growthManager.Initialize();
    }
}
