using UnityEngine;

[System.Serializable]
public class Stage : ScriptableObject
{
    [SerializeField] private Song song;
    [SerializeField] private EnemySpawn[] enemySpawns;
    [SerializeField] private ClearPartOfStage[] clearPartOfStageEvent;
    [SerializeField] private int victoryBeat;
}

[System.Serializable]
public abstract class StageEvent
{
    public int beat;
}

[System.Serializable]
public class EnemySpawn : StageEvent
{
    public GameObject enemy;
    public Vector2 spawnPosition;
}

[System.Serializable]
public class ClearPartOfStage : StageEvent
{
    public int untilPosition;
}

public class Victory : StageEvent
{

}

[System.Serializable]
public class PowerUp : StageEvent
{
   
}