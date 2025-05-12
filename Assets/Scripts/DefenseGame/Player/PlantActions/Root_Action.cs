using UnityEngine;

public class Root_Action : PlantAction
{
    public override void Activate(Plant plant)
    {
        Debug.Log("Root Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
