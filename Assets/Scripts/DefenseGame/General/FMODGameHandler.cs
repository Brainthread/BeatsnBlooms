using UnityEngine;
using UnityEngine.SceneManagement;

public class FMODGameHandler : MonoBehaviour
{
     private MusicManager musicManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private GrowthManager growthManager;
    [SerializeField] private Stage stage;
    private int beattracker = 0;

    void Start()
    {
        Invoke("InitializeGame", 0.5f);
        EventHandler.current.onBeat += OnBeat;
        beattracker = 0;
    }

    void InitializeGame()
    {
        enemyManager.Initialize(stage.enemySpawns);
        //musicManager.Initialize(stage.song);
        //growthManager.Initialize();
    }

    void OnBeat()
    {
        beattracker += 1;
        if(beattracker == stage.victoryBeat)
        {
            EventHandler.current.Win();
            Invoke("SwitchLevel", 3);
        }
    }

    void SwitchLevel()
    {
        PlayerPrefs.SetInt("Won_Demo", 1);
        SceneManager.LoadScene(0);
    }
}
