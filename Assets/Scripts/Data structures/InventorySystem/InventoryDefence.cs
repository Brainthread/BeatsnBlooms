using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryDefence : MonoBehaviour
{
    /*This script handles setting up the inventory GUI for the defence game state
     * and instantiating the inventory slot objects as well as cleanup when
     * game state changes.
     * 
     * It also holds a refference to the currently selected inventory slot
     */

    private TileAction.TileActionTypes currentType = TileAction.TileActionTypes.ATTACK;
    private DefenceInventorySlot currentInventorySlot = null;

    [SerializeField] GameObject inventoryTogglePrefab;
    ToggleGroup toggleGroup;
    private List<GameObject> currentInventorySlots = new List<GameObject>();

    public void SetupDefenceInventory()
    {
        if (toggleGroup == null) toggleGroup = GetComponentInChildren<ToggleGroup>();
        ResetInventorySlots();

        //Find inventory items with stack size > 0 & instantiate DefenceToggle prefabs
        foreach (TypeWithStackSize typeWithStack in InventorySystem.instance.GetTileTypesWithStackSize())
        {
            if (typeWithStack.stackSize < 1) continue;
            //Debug.Log($"Create inventory toggle with - type: {typeWithStack.type} stack: {typeWithStack.stackSize}");
            GameObject gobj = Instantiate(inventoryTogglePrefab, toggleGroup.transform);
            DefenceInventorySlot settings = gobj.GetComponent<DefenceInventorySlot>();
            settings.SetupToggle(typeWithStack.type, typeWithStack.stackSize, this, toggleGroup);
            currentInventorySlots.Add(gobj);
        }
    }


    private void ResetInventorySlots()
    {
        foreach (GameObject gobj in currentInventorySlots)
        {
            gobj.GetComponent<DefenceInventorySlot>().RemoveToggle();
            Destroy(gobj);
        }
        currentInventorySlots.Clear();
    }

    //We still need the final consume logic:

    //When user uses an action on a sequencer tile
    //-Decrement stack size in toggle object

    //When sequencer activates action:

    //-Consume from inventory system when step activates
    //-Remove toggle object if stack size = 0 when last item consumed

    //GUI
    //-Add icons to inventory
    //-Display icons on step sequencer

    public DefenceInventorySlot GetCurrentInventorySlot()
    {
        return currentInventorySlot;
    }

    public void SetCurrentInventorySlot(DefenceInventorySlot slot)
    {
        currentInventorySlot = slot;
    }

    public DefenceInventorySlot GetInventorySlotByType(TileAction.TileActionTypes actionType)
    {
        foreach(GameObject gobj in currentInventorySlots)
        {
            if(gobj.GetComponent<DefenceInventorySlot>().GetTileType() == actionType)
            {
                return gobj.GetComponent<DefenceInventorySlot>();
            }
        }
        return null;
    }

    private void Update()
    {
        //Use key input to toggle inventory item selection...
    }

}

//https://stackoverflow.com/questions/52739763/how-to-get-selected-toggle-from-toggle-group