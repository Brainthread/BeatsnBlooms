using UnityEngine;

public class DefenceInventoryToggle : MonoBehaviour
{
    [SerializeField] private TileAction.TileActionTypes tileType;
    [SerializeField] private InventoryDefence inventoryDefence;

    private void Start()
    {
        inventoryDefence = GetComponentInParent<InventoryDefence>();
    }

    //Event for when toggle activates that tells inventory defence which tiletype has been
    //activated. Then inventoryDefence mediates which inventory item is active
    //for when user clicks on sequencer
    //We need protection for selecting toggles that are stacksize 0
}
