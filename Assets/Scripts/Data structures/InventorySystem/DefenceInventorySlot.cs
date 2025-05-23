﻿using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class DefenceInventorySlot : MonoBehaviour
{
    /*This script manages an individual inventory slot in the defence game state.
     * -Updates GUI for inventory slot
     * -Mediates item consumption & stack size
     * -Removes itself when slot empty
     */

    //ToDo GUI
    //-Add icons to inventory
    //-Display icons on step sequencer

    [SerializeField] private TileAction.TileActionTypes tileType;
    private int stackSize = 0;
    private TMP_Text stackSizeUI;
    private UnityEvent<DefenceInventorySlot> onToggleOn = new UnityEvent<DefenceInventorySlot>();
    private int reserveBuffer = 0;
    private bool isInfinite = false;
    public bool IsInfinite { get { return isInfinite; } set { isInfinite = value; } }

    //Buffer for remove
    private Toggle toggleRef;
    private ToggleGroup groupRef;

    public void ToggleOn(bool on)
    {
        if (!on) return;
        onToggleOn.Invoke(this);
    }

    public void SetupToggle(TileAction.TileActionTypes type, int stackAmt, ToggleGroup toggleGroup)
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.group = toggleGroup;
        toggleGroup.RegisterToggle(toggle);
        onToggleOn.AddListener(InventoryManager.instance.inventoryDefence.SetCurrentInventorySlot);
        stackSizeUI = GetComponentInChildren<TMP_Text>();

        tileType = type;
        stackSize = stackAmt;
        stackSizeUI.text = stackAmt.ToString();

        SetInstances(toggle, toggleGroup);

        if (isInfinite)
        {
            stackSizeUI.text = "∞";
        }
    }

    public TileAction.TileActionTypes GetTileType()
    {
        return tileType;
    }

    public void RemoveToggle()
    {
        groupRef.UnregisterToggle(toggleRef);
    }

    public void ReserveAction()
    {
        stackSizeUI.text = "∞";
        if (!isInfinite)
        {
            stackSize--;
            reserveBuffer++;
            stackSizeUI.text = stackSize.ToString();
        }
    }

    public int GetAvailableStack()
    {
        return stackSize;
    }

    public void UnreserveAction()
    {
        stackSizeUI.text = "∞";
        if (!isInfinite)
        {
            reserveBuffer--;
            stackSize++;
            stackSizeUI.text = stackSize.ToString();
        }
    }

    public void Consume()
    {
        if (!isInfinite)
        {
            reserveBuffer--;
            InventoryItem item = InventorySystem.instance.GetItemByTileType(tileType);
            if (item == null) Debug.LogError("Something went wrong, nothing in inventory of type: " + tileType);
            InventorySystem.instance.RemoveFromInventory(item);

            if (reserveBuffer < 1 && stackSize < 1)
            {
                //Flag for removal, this item is gone...
                //Debug.Log("All item consumed for type: " + tileType);
                groupRef.UnregisterToggle(toggleRef);
                InventoryManager.instance.inventoryDefence.RemoveInventorySlot(gameObject);
                Destroy(gameObject);
            }
        }
    }

    //Because order function calls we can't do this in Start or Awake...
    private void SetInstances(Toggle toggle, ToggleGroup group)
    {
        toggleRef = toggle;
        groupRef = group;
    }
}
