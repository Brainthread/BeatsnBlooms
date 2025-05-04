using UnityEngine;

public class PlantGrowthHandler : MonoBehaviour
{
    private Seed[] seeds;
    private void Start()
    {
        seeds = new Seed[GridManager.current.GetRows()];
    }
    public void PlantSeed (GameObject seedPrefab, int row)
    {
        seeds[row] = Instantiate(seedPrefab, transform).GetComponent<Seed>();
    }
    public void GrowthTick(int row)
    {
        if (seeds[row])
        {
            seeds[row].Grow();
        }
    }
    public void OnGrownUp(int row)
    {
        GameObject grownPrefab = Instantiate(seeds[row].GrownPrefab);
    }
}

