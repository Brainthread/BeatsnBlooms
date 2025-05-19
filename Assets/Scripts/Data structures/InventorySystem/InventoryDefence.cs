using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using static TMPro.Examples.ObjectSpin;

public class InventoryDefence : MonoBehaviour
{
    private TileAction.TileActionTypes currentType = TileAction.TileActionTypes.ATTACK;
    [SerializeField] GameObject inventoryTogglePrefab;
    ToggleGroup toggleGroup;
    private List<GameObject> currentInventorySlots = new List<GameObject>();

    private void Start()
    {
    }

    public void SetupDefenceInventory()
    {
        if (toggleGroup == null) toggleGroup = GetComponentInChildren<ToggleGroup>();
        ResetInventorySlots();



        { //Hacky solution to place basic attack first in the queue, since Dictionary has no internal ordering.
            GameObject gobj = Instantiate(inventoryTogglePrefab, toggleGroup.transform);
            DefenceInventoryToggle settings = gobj.GetComponent<DefenceInventoryToggle>();
            settings.SetupInfiniteToggle(TileAction.TileActionTypes.ATTACK, 1, this, toggleGroup);
            currentInventorySlots.Add(gobj);
        }

        //Find inventory items with stack size > 0 & instantiate DefenceToggle prefabs
        foreach (TypeWithStackSize typeWithStack in InventorySystem.instance.GetTypesWithStackSize())
        {
            if (typeWithStack.stackSize < 1 || typeWithStack.type == TileAction.TileActionTypes.ATTACK) continue;
            GameObject gobj = Instantiate(inventoryTogglePrefab, toggleGroup.transform);
            DefenceInventoryToggle settings = gobj.GetComponent<DefenceInventoryToggle>();
            settings.SetupToggle(typeWithStack.type, typeWithStack.stackSize, this, toggleGroup);
            currentInventorySlots.Add(gobj);
        }
    }

    private void ResetInventorySlots()
    {
        foreach (GameObject gobj in currentInventorySlots)
        {
            gobj.GetComponent<DefenceInventoryToggle>().RemoveToggle();
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

    internal void ConsumeItem(TileAction.TileActionTypes actionType)
    {
        TestItem myItem = GetActionItemFromInventory(actionType);
        if (myItem)
        {
            InventorySystem.instance.RemoveFromInventory(myItem);
        }
    }

    internal bool IsStackAvailable(TileAction.TileActionTypes actionType)
    {
        TestItem myItem = GetActionItemFromInventory(actionType);
        if (myItem)
        {
            if(InventorySystem.instance.HasStack(myItem))
                return true;
        }
        return false;
    }

    private TestItem GetActionItemFromInventory (TileAction.TileActionTypes actionType)
    {
        TestItem[] testitems = InventorySystem.instance.GetComponents<TestItem>();
        TestItem myItem = null;

        for (int i = 0; i < testitems.Length; i++)
        {
            if (actionType == testitems[i].GetTileType())
            {
                myItem = testitems[i];
            }
        }
        return myItem;
    }
}

//https://stackoverflow.com/questions/52739763/how-to-get-selected-toggle-from-toggle-group