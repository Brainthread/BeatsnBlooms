using UnityEngine;

public class SpikeBarrier_Action : PlantAction
{
    public override void Activate(Plant plant)
    {
        Debug.Log("Spike Barrier Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
