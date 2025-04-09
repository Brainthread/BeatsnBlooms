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
    [SerializeField] private int rows = 1;
    [SerializeField] private int columns = 4;

    [SerializeField] private int[] sequencerBoxStates;
    [SerializeField] private GameObject[] representations;

    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    [SerializeField] private Material[] activationMaterials;

    private void Start()
    {
        markerIndex = -1;
        sequencerBoxStates = new int[rows*columns];
        for (int i = 0; i < rows * columns; i++)
        {
            sequencerBoxStates[i] = 0;
            representations[i].GetComponent<SequencerTile>().SetID(i);
        }
        musicManager = MusicManager.instance;
        EventHandler.current.onBeat += OnNewBeat;
        EventHandler.current.onClickSequencerTile += OnTileClicked;
    }

    private void OnNewBeat()
    {
        if(markerIndex != -1)
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
                EventHandler.current.ActivatePlant(i);
            }
        }
    }

    private void OnTileClicked(int id)
    {
        sequencerBoxStates[id] += 1;
        if (sequencerBoxStates[id]>=activationMaterials.Length)
        {
            sequencerBoxStates[id] = 0;
        }
        representations[id].GetComponent<SequencerTile>().SetInnerMaterial(activationMaterials[sequencerBoxStates[id]]);
    }
}
