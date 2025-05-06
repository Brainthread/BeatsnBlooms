using UnityEngine;
using System.Collections.Generic;


public class InventorySystem : MonoBehaviour
{
    public static InventorySystem InventoryInstance;

    //Tile Inventory
    private List<ITile> TileInventory = new List<ITile>();
    //Other Inventory
    private List<IInventoryItem> ItemInventory = new List<IInventoryItem>();

    void Awake()
    {
        if (InventoryInstance == null) InventoryInstance = this;
        else Destroy(this);
    }

    public void AddToInventory(IInventoryItem item)
    {
        ITile tile = (ITile)item;
        if (tile != null)
        {
            ITile found = TileInventory.Find(x => x.Type == tile.Type);
            if (found != null) found.StackSize += tile.StackSize;
            else TileInventory.Add(tile);
            return;
        }
       
        ItemInventory.Add(item);
    }
}

//Inventory Interfaces
public interface IInventoryItem
{
    //public void PickUp();
}
public interface ITile : IInventoryItem
{
    public void PlaceTile();
    public bool IsSelected { get; set; }
    public TILE_TYPE Type { get; set; }
    public int StackSize { get; set; }
}

public enum TILE_TYPE
{
    //Defence
    ROOT,
    BARRIER,
    SPIKE_BARRIER,
    STICKY_SLIME,
    //Ranged
    EXPLOSIVE,
    STICKY_PROJECTILE,
    BEAM,

}

public interface IPlantable : IInventoryItem
{
    public float GrowRate { get; set; }
}