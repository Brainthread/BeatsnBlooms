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

    private void Start()
    {
        EventHandler.current.onBeat += OnNewBeat;
        EventHandler.current.onClickSequencerTile += OnTileClicked;
        EventHandler.current.onStartSong += OnStartSong;
    }

    private void OnStartSong()
    {
        availableTiles = 4;
        markerIndex = -1;
        sequencerBoxActionStates = new int[rows*columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int indexer = i * rows + j;
                sequencerBoxActionStates[indexer] = 0;
                representations[indexer].GetComponent<SequencerTile>().SetID(indexer);
                representations[indexer].name = "Tile"+indexer;
                representations[indexer].GetComponent<SequencerTile>().SetBorderMaterial(inactiveMaterial);
                representations[indexer].GetComponent<SequencerTile>().SetInnerMaterial(tileActions[0].stateMaterial);
                representations[indexer].transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(j, i));
            }
        }
        //musicManager = MusicManager.instance;
    }

    private void OnNewBeat()
    {
        if (markerIndex != -1)
        for(int i = 0; i < rows; i++)
        {
            GameObject rep = representations[i * rows + markerIndex];
            if(rep.activeSelf)
                rep.GetComponent<SequencerTile>().SetBorderMaterial(inactiveMaterial);
        }
        markerIndex += 1;
        if(markerIndex > columns-1)
        {
            markerIndex = 0;
        }
        
        for (int i = 0; i < rows; i++)
        {
            GameObject rep = representations[i * rows + markerIndex];
            if (rep.activeSelf)
            {
                rep.GetComponent<SequencerTile>().SetBorderMaterial(activeMaterial);
                TileAction myState = tileActions[sequencerBoxActionStates[i * rows + markerIndex]];
                switch (myState.tileState)
                {
                    case TileAction.TileState.attack:
                        EventHandler.current.ActivatePlant(i);
                        break;
                    case TileAction.TileState.grow:
                        EventHandler.current.GrowPlant(i);
                        break;
                }
            }
        }
        
    }

    private void OnTileClicked(int id)
    {
        if(sequencerBoxActionStates[id] == 0)
        {
            if (availableTiles == 0)
                return;
            sequencerBoxActionStates[id] += 1;
            availableTiles -= 1;
        }
        else
        {
            sequencerBoxActionStates[id] += 1;
            if (sequencerBoxActionStates[id] == tileActions.Length)
            {
                sequencerBoxActionStates[id] = 0;
                availableTiles += 1;
            }
        }
        TileAction state = tileActions[sequencerBoxActionStates[id]];
        representations[id].GetComponent<SequencerTile>().SetInnerMaterial(state.stateMaterial);
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
    public TileState tileState;
}
