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
    private int rows;

    private void Start()
    {
        musicManager = MusicManager.instance;
        EventHandler.current.onBeat += OnNewBeat;
    }

    private void OnNewBeat()
    {
        
    }
}
