using UnityEngine;


[CreateAssetMenu(fileName = "Stage", menuName = "BeatsAndBlooms/Stage", order = 0)]
[System.Serializable]
public class Stage : ScriptableObject
{
    public Song song;
    public EnemySpawn[] enemySpawns;
    public ClearPartOfStage[] clearPartOfStageEvent;
    public int victoryBeat;
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
    public int untilXPosition;
}

public class Victory : StageEvent
{

}

[System.Serializable]
public class PowerUp : StageEvent
{
   
}