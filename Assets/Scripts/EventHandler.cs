using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public static EventHandler current;
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

}
