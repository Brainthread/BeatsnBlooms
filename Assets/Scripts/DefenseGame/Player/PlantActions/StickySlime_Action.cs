using UnityEngine;

public class NewMonoBehaviourScript : PlantAction
{
    public override void Activate(Plant plant)
    {
        Debug.Log("Sticky Slime Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
