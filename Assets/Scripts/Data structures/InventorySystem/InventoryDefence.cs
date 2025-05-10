using UnityEngine;
using UnityEngine.UI;

public class InventoryDefence : MonoBehaviour
{
    private TileAction.TileActionTypes currentType = TileAction.TileActionTypes.ATTACK;
    [SerializeField] GameObject inventoryTogglePrefab;
    ToggleGroup toggleGroup;

    public void SetupDefenceInventory()
    {
        if (toggleGroup == null) toggleGroup = GetComponentInChildren<ToggleGroup>();
        //Find inventory items with stack size > 0 & instantiate DefenceToggle prefabs
        foreach (TypeWithStackSize typeWithStack in InventorySystem.instance.GetTypesWithStackSize())
        {
            if (typeWithStack.stackSize < 1) continue;
            //Debug.Log($"Create inventory toggle with - type: {typeWithStack.type} stack: {typeWithStack.stackSize}");
            GameObject gobj = Instantiate(inventoryTogglePrefab, toggleGroup.transform);
            DefenceInventoryToggle settings = gobj.GetComponent<DefenceInventoryToggle>();
            settings.SetupToggle(typeWithStack.type, typeWithStack.stackSize, this, toggleGroup);
        }
    }

    //We still need the final consume logic
    //When sequencer activates action:
    //-Consume from inventory system
    //-Update stack size in toggle object
    //-Remove toggle object if stack size = 0

    //GUI
    //-Add icons to inventory
    //-Display icons on step sequencer

    public void SetCurrentTileType(TileAction.TileActionTypes type)
    {
        Debug.Log("Setting Defence Tile: " + type);
        currentType = InventorySystem.instance.GetTotalTileStackSize(type) > 0 ? type : TileAction.TileActionTypes.ATTACK;
    }

    public TileAction.TileActionTypes GetCurrentTileType()
    {
        return currentType;
    }

    private void Update()
    {
        //Use key input to toggle inventory item selection...
    }

}

//https://stackoverflow.com/questions/52739763/how-to-get-selected-toggle-from-toggle-group