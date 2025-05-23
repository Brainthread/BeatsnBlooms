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

    public event Action<int> onUnClickSequencerTile;
    public void UnClickSequencerTile(int id)
    {
        if (onUnClickSequencerTile != null)
        {
            onUnClickSequencerTile(id);
        }
    }

    public event Action<int, TileAction.TileActionTypes> onPlantActivation;
    public void ActivatePlant(int position, TileAction.TileActionTypes actionType)
    {
        if(onPlantActivation != null)
        {
            onPlantActivation(position, actionType);
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


    public event Action<bool> onGeneratorDanger;
    public void GeneratorDanger(bool value)
    {
        if (onGeneratorDanger != null)
        {
            onGeneratorDanger(value);
        }
    }

    public event Action onLoss;
    public void Lose ()
    {
        if(onLoss != null)
        {
            onLoss();
        }
    }

    public event Action onGeneratorDestroyed;
    public void GeneratorDestroyed()
    {
        if (onGeneratorDestroyed != null)
        {
            onGeneratorDestroyed();
        }
    }

    public event Action<int> onGrowPlant;
    internal void GrowPlant(int row)
    {
        if(onGrowPlant != null)
        {
            onGrowPlant(row);
        }
    }

    public event Action<int> onPlayerUnclickRow;
    public void OnPlayerUnclickRow(int row)
    {
        if(onPlayerUnclickRow != null)
        {
            onPlayerUnclickRow(row);
        }
    }

    public event Action<int> onPlayerClickRow;
    public void OnPlayerClickRow(int row)
    {
        if(onPlayerClickRow != null)
        {
            onPlayerClickRow(row);
        }
    }

    public event Action onWin;
    public void Win()
    {
        if(onWin != null)
        {
            onWin();
        }
    }
}
