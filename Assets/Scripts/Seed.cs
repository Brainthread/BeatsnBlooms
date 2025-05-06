using UnityEngine;

public class Seed : MonoBehaviour
{
    private PlantGrowthHandler growthHandler;
    [SerializeField] private GameObject grownPrefab;
    public GameObject GrownPrefab
    {
        get { return grownPrefab; }
    }
    private int row = -1;
    public int Row
    {
        set { row = value; }
    }
    public PlantGrowthHandler GrowthHandler
    {
        set { growthHandler = value; }
    }
    [SerializeField] private int growthStage = 0;
    [SerializeField] private int maxGrowthStage = 2;
    public void Grow()
    {
        growthStage += 1;
        if (growthStage >= maxGrowthStage)
        {
            growthHandler.OnGrownUp(row);
            Destroy(gameObject);
        }
    }
}
