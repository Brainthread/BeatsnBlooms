using UnityEngine;
using System.Collections.Generic;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem InventoryInstance;

    private Dictionary<string, int> tileInventory = new Dictionary<string, int>();
    //private Tile
    //foreach(TileAction.TileActionTypes tileType in Enum.GetValues(typeof(TileAction.TileActionTypes)){}
    //Array arr = Enum.GetNames(typeof(TileAction.TileActionTypes));
    


    void Awake()
    {
        if (InventoryInstance == null) InventoryInstance = this;
        else Destroy(this);
    }

    private void Start()
    {
        foreach (string entry in Enum.GetNames(typeof(TileAction.TileActionTypes)))
        {
            tileInventory.Add(entry, 0);
        }
    }

    public void AddToInventory()
    {

    }

    public void RemoveFromInventory()
    {

    }

    private void AddTileToInventory(TileAction.TileActionTypes actionType, int stackSize)
    {
        tileInventory[actionType.ToString()] += stackSize;
    }
}

