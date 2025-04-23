using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager current;
    [SerializeField] private int lanes = 4;
    [SerializeField] private float laneDistance = 12;
    [SerializeField] private float laneStartPosition = 18;
    [SerializeField] private float[] laneHeights;

    private void Start()
    {
        current = this;
        laneHeights = new float[lanes];
        for(int i = 0; i < lanes; i++)
        {
            laneHeights[i] = laneStartPosition - laneDistance * i;
        }
    }
    public float[] GetLaneHeights()
    {
        return laneHeights;
    }
}
