using UnityEngine;

public class PlantGrowthHandler : MonoBehaviour
{
    private PlantManager plantManager;
    [SerializeField] private Seed[] seeds;
    [SerializeField] private int[] plantPositions;
    public int[] PlantPositions { get { return plantPositions; } }
    [SerializeField] private bool showGrowPositionIndicator;
    [SerializeField] private GameObject growPositionIndicator;
    private void Start()
    {
        EventHandler.current.onGrowPlant += OnGrowPlant;
        plantManager = GetComponent<PlantManager>();
        int rows = GridManager.current.GetRows();
        plantPositions = new int[rows];
        for(int i = 0; i < plantPositions.Length; i++)
        {
            plantPositions[i] = 4;
        }
        seeds = new Seed[rows];
    }
    public void PlantSeed (GameObject seedPrefab, int row)
    {
        if (seeds[row])
        {
            Destroy(seeds[row].gameObject);
        }
        plantManager.DestroyPlant(row);
        seeds[row] = Instantiate(seedPrefab, transform).GetComponent<Seed>();
        seeds[row].GetComponent<Seed>().Row = row;
        seeds[row].GrowthHandler = this;
        seeds[row].transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(plantPositions[row], row)); 
    }

    public void ShowPlantIndicator (bool value)
    {
        growPositionIndicator.SetActive(value);
    }


    public void SetPlantIndicatorPosition (Vector3 value)
    {
        growPositionIndicator.transform.position = value;
    }

    public void LosePosition(int row)
    {
        plantPositions[row] -= 1;
    }
    public void OnGrowPlant(int row)
    {
        GrowthTick(row);
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
        GameObject grownPrefab = Instantiate(seeds[row].GrownPrefab, transform);
        grownPrefab.transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(plantPositions[row], row));
        plantManager.SetPlant(row, grownPrefab);
    }
}

