using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

class FMOD_TimelineCallbacks : MonoBehaviour
{
    [SerializeField]
    private FMOD_Instantiator masterMusicEvent;
    private FMOD.Studio.EventInstance eventInstance;

    //Event Hooks
    [SerializeField] private UnityEvent<int> onBeatEvent;
    [SerializeField] private UnityEvent<string> onMarkerEvent;

    //Note division enum, whole, half, quarter?

    //Readable buffers
    private int currentBeat = 0;
    private int totalBeats = 0;
    private string currentMarker = null;

    //Storage class for buffering timeline callback event data, 
    //Managed memory, DONT PUT ANYTHING WITH EXTERNAL REFERENCES IN HERE!
    [StructLayout(LayoutKind.Sequential)]
    class TimelineDataBuffer
    {
        //BPM, time, nested events, etc... if needed
        public int currentBeat = 0;
        public int beatBuffer = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    //Data Buffer Instances
    TimelineDataBuffer timelineDataInstance;
    GCHandle timelineHandle;
    FMOD.Studio.EVENT_CALLBACK timelineDataCallback;

    void OnDestroy()
    {
        //Garbage Collection
        eventInstance.setUserData(IntPtr.Zero);
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        eventInstance.release();
        timelineHandle.Free();
    }

    private void Update()
    {
        extractCallbackBuffer();
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT TimelineEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineDataPtr;
        FMOD.RESULT result = instance.getUserData(out timelineDataPtr);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
            return result;
        }
        else if (timelineDataPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineDataPtr);
            TimelineDataBuffer timelineDataInstance = (TimelineDataBuffer)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineDataInstance.currentBeat = parameter.beat;
                        //Debug.Log(parameter.tempo);
                        //Debug.Log(parameter.position);
                        timelineDataInstance.beatBuffer++;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineDataInstance.lastMarker = parameter.name;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED:
                    {
                        timelineHandle.Free();
                        break;
                    }
                    //if we need any other data from the timeline add more cases...
                    //case FMOD.Studio.EVENT_CALLBACK_TYPE.
            }
        }
        return FMOD.RESULT.OK;
    }

    private void extractCallbackBuffer()
    {   
        if(currentBeat != timelineDataInstance.currentBeat)
        {
            currentBeat = timelineDataInstance.currentBeat;
            totalBeats = timelineDataInstance.beatBuffer;
            onBeatEvent.Invoke(currentBeat);
        }

        if(currentMarker != timelineDataInstance.lastMarker)
        {
            //Note this structure will only fire if a NEW marker encountered
            //will NOT retrigger when same marker encountered again for instance in a loop...
            currentMarker = timelineDataInstance.lastMarker;
            onMarkerEvent.Invoke(currentMarker);
        }
    }
    public void SetEventInstance(FMOD.Studio.EventInstance instance)
    {
        eventInstance = instance;

        //Instantiate data buffer & copy event hooks
        timelineDataInstance = new TimelineDataBuffer();

        //Instantiate our timeline callback
        timelineDataCallback = new FMOD.Studio.EVENT_CALLBACK(TimelineEventCallback);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineDataInstance, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        eventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        //Add the callback to the event
        eventInstance.setCallback(timelineDataCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //FMOD.Studio.EVENT_CALLBACK_TYPE... //add other callback types if necessary
    }
}

//Some resources for more complex needs
//https://qa.fmod.com/t/is-it-possible-to-subscribe-to-sub-beat-callbacks/19403/15

