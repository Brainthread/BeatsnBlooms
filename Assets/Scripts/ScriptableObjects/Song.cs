using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "BeatsAndBlooms/Song", order = 0)]
public class Song : ScriptableObject
{
    public AudioClip audio;
    public float bpm;
    public int endBeat;
    public enum TimeSignature
    {
        FOUR_OVER_FOUR,
        THREE_OVER_FOUR
    }
}

[System.Serializable]
public class SongEvent
{
    public int beat;
    public enum BeatType
    {
        STRONG,
        WEAK
    }
    public BeatType beatType;
}
