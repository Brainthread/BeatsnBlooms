using UnityEngine;

public class PlantActionHandler : MonoBehaviour
{
    public static PlantActionHandler instance;
    [SerializeField] PlantAction AttackAction;
    //[SerializeField] PlantAction GrowAction;
    [SerializeField] PlantAction RootAction;
    [SerializeField] PlantAction SpikeBarrierAction;
    [SerializeField] PlantAction StickySlimeAction;
    [SerializeField] PlantAction ExplosiveAction;
    [SerializeField] PlantAction StickyProjectileAction;
    [SerializeField] PlantAction BeamAction;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public PlantAction GetAction(TileAction.TileActionTypes actionType)
    {
        switch (actionType)
        {
            case TileAction.TileActionTypes.ATTACK:
                return AttackAction;
            case TileAction.TileActionTypes.ROOT:
                return RootAction;
            case TileAction.TileActionTypes.BARRIER:
                return SpikeBarrierAction;
            case TileAction.TileActionTypes.SPIKE_BARRIER:
                return SpikeBarrierAction;
            case TileAction.TileActionTypes.STICKY_SLIME:
                return StickySlimeAction;
            case TileAction.TileActionTypes.EXPLOSIVE:
                return ExplosiveAction;
            case TileAction.TileActionTypes.STICKY_PROJECTILE:
                return StickProjectileAction;
            case TileAction.TileActionTypes.BEAM:
                return BeamAction;
            default:
                throw new System.Exception("No matching action: " + actionType);
        }
    }
}
