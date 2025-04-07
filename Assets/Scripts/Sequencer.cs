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

    private int[][] sequencerBoxStates;
    [SerializeField] private GameObject[] representations;

    [SerializeField] private Material active_material;
    [SerializeField] private Material inactive_material;

    private void Start()
    {
        markerIndex = -1;
        sequencerBoxStates = new int[rows][];
        for(int i = 0; i < rows; i++)
        {
            sequencerBoxStates[i] = new int[columns];
            for(int j = 0; j < columns; j++)
            { 
                sequencerBoxStates[i][j] = 0;
            }
         
        }
        musicManager = MusicManager.instance;
        EventHandler.current.onBeat += OnNewBeat;
    }

    private void OnNewBeat()
    {
        if(markerIndex != -1)
        for(int i = 0; i < rows; i++)
        {
            representations[i*rows + markerIndex].GetComponent<Renderer>().material = inactive_material;
        }
        markerIndex += 1;
        if(markerIndex > columns-1)
        {
            markerIndex = 0;
        }
        for (int i = 0; i < rows; i++)
        {
            representations[i*rows + markerIndex].GetComponent<Renderer>().material = active_material;
        }
    }
}
