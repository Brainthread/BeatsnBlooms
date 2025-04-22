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

    [SerializeField] private int[] sequencerBoxStates;
    [SerializeField] private GameObject[] representations;

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    [SerializeField] private Material[] stateMaterials;


    private void Start()
    {
        EventHandler.current.onBeat += OnNewBeat;
        EventHandler.current.onClickSequencerTile += OnTileClicked;
        EventHandler.current.onStartSong += OnStartSong;
    }

    private void OnStartSong()
    {
        Debug.Log("begin start song");
        availableTiles = 4;
        markerIndex = -1;
        sequencerBoxStates = new int[rows*columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int indexer = i * rows + j;
                sequencerBoxStates[indexer] = 0;
                representations[indexer].GetComponent<SequencerTile>().SetID(indexer);
                representations[indexer].name = "Tile"+indexer;
                representations[indexer].GetComponent<SequencerTile>().SetBorderMaterial(inactiveMaterial);
                representations[indexer].GetComponent<SequencerTile>().SetInnerMaterial(stateMaterials[0]);
                representations[indexer].transform.position = GridManager.current.GridPositionToWorldPosition(new Vector2(j, i));
            }
        }
        //musicManager = MusicManager.instance;
        Debug.Log("end start song");
    }

    private void OnNewBeat()
    {
        Debug.Log("begin on new beat");
        if (markerIndex != -1)
        for(int i = 0; i < rows; i++)
        {
            representations[i*rows + markerIndex].GetComponent<SequencerTile>().SetBorderMaterial(inactiveMaterial);
        }
        markerIndex += 1;
        if(markerIndex > columns-1)
        {
            markerIndex = 0;
        }
        
        for (int i = 0; i < rows; i++)
        {
            representations[i*rows + markerIndex].GetComponent<SequencerTile>().SetBorderMaterial(activeMaterial);
            
            if (sequencerBoxStates[i*rows + markerIndex] != 0)
            {
                EventHandler.current.ActivatePlant(i, sequencerBoxStates[i * rows + markerIndex]-1);
            }
        }
        
        Debug.Log("end on new beat");
    }

    private void OnTileClicked(int id)
    {
        Debug.Log("begin clicked");
        if (availableTiles > 0)
        {
            if (sequencerBoxStates[id] == 0)
            {
                availableTiles -= 1;
            }
            sequencerBoxStates[id] += 1;
            if (sequencerBoxStates[id] >= stateMaterials.Length)
            {
                sequencerBoxStates[id] = 0;
                availableTiles += 1;
            }
        }
        else
        {
            if (sequencerBoxStates[id] + 1 >= stateMaterials.Length)
            {
                sequencerBoxStates[id] = 0;
                availableTiles += 1;
            }
        }
        representations[id].GetComponent<SequencerTile>().SetInnerMaterial(stateMaterials[sequencerBoxStates[id]]);
        Debug.Log("end new beat");
    }
}
