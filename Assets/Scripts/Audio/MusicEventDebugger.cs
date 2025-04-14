using UnityEngine;
using System;

public class MusicEventDebugger : MonoBehaviour
{
    private string marker;
    private int beat;

    /*
    [SerializeField]
    [Range(1, 16)]
    private int beatDiv = 4;
    */

    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Beat = {0}, Last Marker = {1}", beat, marker));
    }

    /*
    int CalcBeat(int beatnum)
    {
        int wrapped = beatnum % 16;
        return 0;
    }
    */

    public void SetBeat(int beatNum)
    {
        beat = beatNum;
    }

    public void SetMarker(string markerName)
    {
        marker = markerName;
    }
}
