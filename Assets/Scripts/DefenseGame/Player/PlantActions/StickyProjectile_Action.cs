using UnityEngine;

public class StickyProjectile_Action : PlantAction
{
    public override void Activate(Plant plant)
    {
        Debug.Log("Sticky Projectile Action Execute Goes Here");
    }

    public override Texture2D GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
