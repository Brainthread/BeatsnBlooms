using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private GameObject onDestroySpawn;

    private void Start()
    {
        GetComponent<HealthManager>().onHealthDepleted += OnDeath;
    }
    void OnDeath(GameObject g)
    {
        if (onDestroySpawn)
        {
            Instantiate(onDestroySpawn, transform.position, transform.rotation);
        }
        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
