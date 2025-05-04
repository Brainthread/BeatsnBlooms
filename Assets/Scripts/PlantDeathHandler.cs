using UnityEngine;

[RequireComponent(typeof(Plant))]
public class PlantDeathHandler : MonoBehaviour
{
    [SerializeField] private GameObject onDestroySpawn;
    private Plant myPlant;
    private void Start()
    {
        myPlant = GetComponent<Plant>();
        GetComponent<HealthManager>().OnHealthDepleted += OnDeath;
    }
    void OnDeath(GameObject g)
    {
        if (onDestroySpawn)
        {
            Instantiate(onDestroySpawn, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
