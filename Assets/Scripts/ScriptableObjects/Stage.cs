using UnityEngine;

[System.Serializable]
public class Stage : ScriptableObject
{
    [SerializeField] private Song song;
     
}

[System.Serializable]
public abstract class StageEvent
{
    
}

[System.Serializable]
public class EnemySpawn : StageEvent
{

}

[System.Serializable]
public class PowerUp : StageEvent
{

}