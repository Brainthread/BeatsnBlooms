using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;
    private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<string, List<TileItem>> tileInventory = new Dictionary<string, List<TileItem>>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (string name in Enum.GetNames(typeof(TileAction.TileActionTypes)))
        {
            tileInventory.Add(name, new List<TileItem>());
        }
    }

    //------------------------------Inventory Management------------------------------//
    public void AddToInventory(InventoryItem item)
    {
        if(item as TileItem != null)
        {
            AddTileToInventory(item as TileItem);
            return;
        }

        inventory.Add(item);
    }

    public void RemoveFromInventory(InventoryItem item)
    {
        if (item as TileItem != null)
        {
            AddTileToInventory(item as TileItem);
            return;
        }

        inventory.Add(item);
    }

    private void AddTileToInventory(TileItem item)
    {
        tileInventory[item.GetTileType().ToString()].Add(item);
        Debug.Log(item.GetTileType().ToString() + ": " + GetTotalTileStackSize(item.GetTileType()));
    }

    private bool RemoveTileFromInventory(TileItem item)
    {
        if (!tileInventory[item.GetTileType().ToString()].Last().Consume())
        {
            return tileInventory[item.GetTileType().ToString()].Remove(item);
        }
        return true;
    }

    //--------------------------------Inventory Helpers-------------------------------//
    public int GetTotalTileStackSize(TileAction.TileActionTypes actionType)
    {
        int total = 0;
        foreach(TileItem item in tileInventory[actionType.ToString()])
        {
            total += item.GetStackSize();
        }
        return total;
    }
}

public abstract class InventoryItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        InventorySystem.instance.AddToInventory(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        InventorySystem.instance.AddToInventory(this);
        Destroy(gameObject);
    }
}

public abstract class TileItem : InventoryItem
{
    [SerializeField] private TileAction.TileActionTypes tileType;
    [SerializeField] private int stackSize = 1;

    public TileAction.TileActionTypes GetTileType() 
    {
        return tileType;
    }

    public int GetStackSize()
    {
        return stackSize;
    }

    public bool Consume()
    {
        if(stackSize > 0)
        {
            stackSize--;
            return true;
        }
        return false;
    }
}
