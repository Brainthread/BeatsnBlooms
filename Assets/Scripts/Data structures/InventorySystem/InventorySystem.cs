using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class InventorySystem : MonoBehaviour
{
    /*This is the core of the inventory system.
     * -Persistent between scenes
     * -Accessible via static ref
     * -Manages inventory addition & removal
     * -Contains abstract class definitions for different inventory item types
     * -Helpers for getting information about inventory state
     */
    public static InventorySystem instance;
    private List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<TileAction.TileActionTypes, List<TileItem>> tileInventory = new Dictionary<TileAction.TileActionTypes, List<TileItem>>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(gameObject);

        foreach (TileAction.TileActionTypes type in Enum.GetValues(typeof(TileAction.TileActionTypes)))
        {
            tileInventory.Add(type, new List<TileItem>());
        }
    }

    private void Start()
    {

    }

    //------------------------------Inventory Management------------------------------//
    public void AddToInventory(InventoryItem item)
    {
        if (item as TileItem != null)
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
            RemoveTileFromInventory(item as TileItem);
            return;
        }

        inventory.Remove(item);
    }

    private void AddTileToInventory(TileItem item)
    {
        tileInventory[item.GetTileType()].Add(item);
        Debug.Log(item.GetTileType().ToString() + ": " + GetTotalTileStackSize(item.GetTileType()));
    }

    private bool RemoveTileFromInventory(TileItem item)
    {
        Debug.Log("Remove Tile");
        if (!tileInventory[item.GetTileType()].Last().Consume())
        {
            return tileInventory[item.GetTileType()].Remove(item);
        }
        return true;
    }

    //--------------------------------Inventory Helpers-------------------------------//
    public int GetTotalTileStackSize(TileAction.TileActionTypes actionType)
    {
        int total = 0;
        foreach (TileItem item in tileInventory[actionType])
        {
            total += item.GetStackSize();
        }
        return total;
    }

    public List<TypeWithStackSize> GetTileTypesWithStackSize()
    {
        List<TypeWithStackSize> typesWithStackSize = new List<TypeWithStackSize>();
        foreach (var key in tileInventory.Keys)
        {
            TypeWithStackSize itemData;
            itemData.type = key;
            itemData.stackSize = GetTotalTileStackSize(key);
            typesWithStackSize.Add(itemData);
        }
        return typesWithStackSize;
    }

    public InventoryItem GetItemByTileType(TileAction.TileActionTypes actionType)
    {
        return tileInventory[actionType].Last();
    }

    public void SetupTestInventory()
    {
        TileItem item1 = gameObject.AddComponent<TestItem>();
        item1.SetupItem(TileAction.TileActionTypes.BARRIER, 10);

        TileItem item2 = gameObject.AddComponent<TestItem>();
        item2.SetupItem(TileAction.TileActionTypes.BEAM, 20);

        TileItem item3 = gameObject.AddComponent<TestItem>();
        item3.SetupItem(TileAction.TileActionTypes.ROOT, 5);

        AddToInventory(item1);
        AddToInventory(item2);
        AddToInventory(item3);
    }
}


//Only for use for tile items. Later refactor for better separation between
//tile inventory and regular inventory...
public struct TypeWithStackSize
{
    public TileAction.TileActionTypes type;
    public int stackSize;
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

    public void SetupItem(TileAction.TileActionTypes type, int stackAmt)
    {
        tileType = type;
        stackSize = stackAmt;
    }

    public bool Consume()
    {
        if (stackSize > 0)
        {
            stackSize--;
            return true;
        }
        return false;
    }
}
