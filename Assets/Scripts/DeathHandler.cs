using UnityEngine;
using UnityEngine.Events;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private bool destroyOnDeath = true;
    [SerializeField] private GameObject onDestroySpawn;
    [SerializeField] private UnityEvent onEnemyDestroyed;

    private void Start()
    {
        GetComponent<HealthManager>().onHealthDepleted += OnDeath;
    }
    void OnDeath()
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

        onEnemyDestroyed.Invoke();
    }
}
