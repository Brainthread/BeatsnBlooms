using UnityEngine;

public class GeneratorDeathHandler : MonoBehaviour
{
    [SerializeField] private GameObject smallExplosion;
    [SerializeField] private GameObject largeExplosion;
    [SerializeField] private Vector3 explosionArea;
    [SerializeField] private int explosions;
    bool dying = false;

    private void Start()
    {
        GetComponent<HealthManager>().OnHealthDepleted += OnDeath;
    }
    void OnDeath()
    {
        if(!dying)
        {
            Explode();
            dying = true;
            EventHandler.current.GeneratorDestroyed();
            float t = 0;
            for (int i = 0; i < explosions; i++)
            {
                float x = i + 1;
                t += Random.Range(0.3f/x, 1.2f/x);
                Invoke("Explode", t);
            }
            Invoke("DestroyGenerator", t + 0.5f);
        }
    }
    void DestroyGenerator()
    {
        EventHandler.current.Lose();
        Destroy(gameObject);
    }
    void Explode()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(-explosionArea.x, explosionArea.x);
        spawnPosition.z += Random.Range(-explosionArea.z, explosionArea.z);
        spawnPosition.y += Random.Range(-explosionArea.y, explosionArea.y);
        Instantiate(smallExplosion, spawnPosition, Quaternion.identity);
    }
}
