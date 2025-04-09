using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private Plant[] plants;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onPlantActivation += OnPlantActivation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPlantActivation(int id)
    {
        
    }
}
