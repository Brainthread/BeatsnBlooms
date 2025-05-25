using UnityEngine;

public class Barrier_Action : PlantAction
{
    public override void Activate(Plant plant)
    {
        onPlantAction.Invoke();
        Debug.Log("Barrier Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
