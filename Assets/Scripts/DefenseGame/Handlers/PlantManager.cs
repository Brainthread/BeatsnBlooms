using UnityEngine;
using System;
public class PlantManager : MonoBehaviour
{
    public static PlantManager current;
    [SerializeField] private Plant[] plants;
    private int plantPosition = 4;
    private Vector3[] gridPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onGrowPlant += OnPlantGrowth;
        current = this;
        gridPositions = new Vector3[plants.Length];
        EventHandler.current.onPlantActivation += OnPlantActivation;
        for(int i = 0; i < plants.Length; i++)
        {
            if (!plants[i])
                continue;
            gridPositions[i] = GridManager.current.GridPositionToWorldPosition(new Vector2(plantPosition, i));
            plants[i].transform.position = gridPositions[i];
            plants[i].Row = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyPlant(int row)
    {
        if (plants[row] != null)
        {
            Destroy(plants[row].gameObject);
        }
    }

    public void OnDestroyPlant(int row)
    {
        plants[row] = null;
    }

    public void SetPlant(int row, GameObject plant)
    {
        plants[row] = plant.GetComponent<Plant>();
    }

    private void OnPlantActivation(int id, PlantAction action, int column)
    {
        if (!plants[id])
            return;
        if (plants[id].gameObject.activeSelf)
            plants[id].Activate(action, column);
    }

    private void OnPlantGrowth (int id)
    {
        if (!plants[id])
            return;
        plants[id].GetComponent<HealthManager>().ApplyHealing(1);
    }
}
