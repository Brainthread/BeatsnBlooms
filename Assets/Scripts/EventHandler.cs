using UnityEngine;
using System;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public static EventHandler current;

    //DAves stuff feel free to change..
    public Event onMusicStartEvent;
    public Event onBeatEvent;
    private void Awake()
    {
        current = this;
    }

    public event Action onBeat;
    public void NewBeat()
    {
        if(onBeat != null)
        {
            onBeat();
        }
    }


    public event Action<int> onClickSequencerTile;
    public void ClickSequencerTile(int id)
    {
        if(onClickSequencerTile != null)
        {
            onClickSequencerTile(id);
        }
    }

    public event Action<int, int> onPlantActivation;
    public void ActivatePlant(int position, int powerup)
    {
        if(onPlantActivation != null)
        {
            onPlantActivation(position, powerup);
        }
    }

    public event Action onStartSong;
    public void StartSong()
    {
        if(onStartSong != null)
        {
            onStartSong();
        }
    }
}
