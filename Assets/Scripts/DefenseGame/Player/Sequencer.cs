using UnityEngine;

/* 
 * 
 * 
 * 
 */

public class Sequencer : MonoBehaviour
{
    private MusicManager musicManager;
    private int markerIndex = 0;
    [SerializeField] private int availableTiles = 4;
    [SerializeField] private int rows = 1;
    [SerializeField] private int columns = 4;

    [SerializeField] private TileAction[] tileActions;
    [SerializeField] private int[] sequencerBoxActionStates;
    [SerializeField] private GameObject[] representations;

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private PlantGrowthHandler plantGrowthManager;

    private void Start()
    {
        EventHandler.current.onBeat += OnNewBeat;
        EventHandler.current.onClickSequencerTile += OnTileClicked;
        EventHandler.current.onUnClickSequencerTile += OnTileUnclicked;
        EventHandler.current.onStartSong += OnStartSong;
        OnStartSong();
    }

    private void OnStartSong()
    {
        availableTiles = 4;
        markerIndex = 0;
        sequencerBoxActionStates = new int[rows * columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int indexer = i * rows + j;
                sequencerBoxActionStates[indexer] = 0;
                SequencerTile tile = representations[indexer].GetComponent<SequencerTile>();
                tile.SetID(indexer);
                tile.SetBorderMaterial(inactiveMaterial);
                tile.SetInnerMaterial(tileActions[0].stateMaterial);
                tile.Sequencer = this;
                representations[indexer].name = "Tile" + indexer;
                representations[indexer].transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(j, i));
            }
        }
        //musicManager = MusicManager.instance;
    }

    private void OnNewBeat()
    {
        for (int i = 0; i < rows; i++)
        {
            GameObject rep = representations[i * rows + markerIndex];
            if (rep.activeSelf)
                rep.GetComponent<SequencerTile>().SetBorderMaterial(inactiveMaterial);
        }
        markerIndex += 1;
        if (markerIndex > columns - 1)
        {
            markerIndex = 0;
        }

        for (int i = 0; i < rows; i++)
        {
            GameObject rep = representations[i * rows + markerIndex];
            if (rep.activeSelf)
            {
                //Debug.Log(i * rows + markerIndex);
                rep.GetComponent<SequencerTile>().SetBorderMaterial(activeMaterial);
                TileAction myState = tileActions[sequencerBoxActionStates[i * rows + markerIndex]];

                switch (myState.tileState)
                {
                    case TileAction.TileState.attack:
                        TileAction.TileActionTypes actionType = rep.GetComponent<SequencerTile>().GetAndConsumePlantAction();
                        EventHandler.current.ActivatePlant(i, actionType);
                        break;
                    case TileAction.TileState.grow:
                        EventHandler.current.GrowPlant(i);
                        break;
                }
            }
        }

    }



    public void TileDestroyed(int id)
    {
        int row = (int)Mathf.Floor((float)id / (float)columns);
        if (plantGrowthManager)
        {
            plantGrowthManager.LosePosition(row);
        }
        TileAction tileAction = tileActions[sequencerBoxActionStates[id]];
        if (tileAction.tileState != TileAction.TileState.unselected && tileAction.tileState != TileAction.TileState.item)
        {
            availableTiles += 1;
        }
    }

    private void OnTileClicked(int id)
    {
        bool selected = true;
        if (sequencerBoxActionStates[id] == 0)
        {
            if (availableTiles == 0)
                return;
            sequencerBoxActionStates[id] += 1;
            availableTiles -= 1;
        }
        else
        {
            selected = false;
            sequencerBoxActionStates[id] += 1;
            if (sequencerBoxActionStates[id] == tileActions.Length)
            {
                sequencerBoxActionStates[id] = 0;
                availableTiles += 1;
            }
        }
        TileAction state = tileActions[sequencerBoxActionStates[id]];
        SequencerTile tile = representations[id].GetComponent<SequencerTile>();
        tile.SetInnerMaterial(state.stateMaterial);

        if (selected) ManageInventoryTileSelected(tile);
        else ManageInventoryTileUnselected(tile);
    }

    private void ManageInventoryTileSelected(SequencerTile tile)
    {
        //Inventory Management
        //If tile stacksize < 1 don't let user add action
        //...
        DefenceInventorySlot slot = InventoryManager.instance.inventoryDefence.GetCurrentInventorySlot();
        if (slot.GetAvailableStack() > 0)
        {
            tile.SetPlantAction(slot.GetTileType());
           //decrement availability on inventory slot
            InventoryManager.instance.inventoryDefence.GetCurrentInventorySlot().ReserveAction();
        }

    }
    private void ManageInventoryTileUnselected(SequencerTile tile)
    {
        //Inventory Management
        //If the tile is unclicked before the item can be consumed add back to stack size on tile
        Debug.Log("unclick tile");
        DefenceInventorySlot slot = InventoryManager.instance.inventoryDefence.GetInventorySlotByType(tile.GetPlantAction());
        if (slot != null) slot.UnreserveAction(); //Again for now we have to protect against null case
        //also if they did switch rather than turning off tile it should just swap items
    }
    private void OnTileUnclicked(int id)
    {
        if (sequencerBoxActionStates[id] != 0)
        {
            sequencerBoxActionStates[id] = 0;
            availableTiles += 1;
        }
        TileAction state = tileActions[sequencerBoxActionStates[id]];
        SequencerTile tile = representations[id].GetComponent<SequencerTile>();
        tile.SetInnerMaterial(state.stateMaterial);
    }

}


/*
 * Data class containing information regarding what action to perform on a beat event.
 */
[System.Serializable]
public class TileAction
{
    public Material stateMaterial;
    public enum TileState
    {
        unselected,
        attack,
        grow,
        item
    }

    public enum TileActionTypes
    {
        //Defence
        ROOT,
        BARRIER,
        SPIKE_BARRIER,
        STICKY_SLIME,
        //Ranged
        ATTACK,
        EXPLOSIVE,
        STICKY_PROJECTILE,
        BEAM,
    }
    public TileState tileState;
    public TileActionTypes actionTypes;
}
