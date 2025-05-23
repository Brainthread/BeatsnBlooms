using UnityEngine;

public class Explosive_Action : PlantAction
{
    [SerializeField] private GameObject fgmSeed;
    public override void Activate(Plant plant)
    {
        Instantiate(fgmSeed, transform.position, transform.rotation);
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
