using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private Plant[] plants;
    private int plantPosition = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onPlantActivation += OnPlantActivation;
        for(int i = 0; i < plants.Length; i++)
        {
            if (!plants[i])
                continue;
            plants[i].transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(plantPosition, i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPlantActivation(int id, int powerup)
    {
        plants[id].Activate(powerup);
    }
}
