using UnityEngine;

public class ProjectileEmitter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnLocation;
    public void FireProjectile()
    {
        Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
}
