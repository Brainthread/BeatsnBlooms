using UnityEngine;
using UnityEngine.Events;

public class ProjectileEmitter : PlantAction
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnLocation;

    /*
    public void FireProjectile()
    {
        Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
    */

    public override void Activate(Plant plant)
    {
        Instantiate(projectile, plant.transform.position, plant.transform.rotation);
        onPlantAction.Invoke();
        //Debug.Log("Projectile Attack");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
