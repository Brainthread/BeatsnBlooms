using UnityEngine;

public class Explosive_Action : PlantAction
{
    public override void Activate(Plant plant)
    {
        Debug.Log("Explosive Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
