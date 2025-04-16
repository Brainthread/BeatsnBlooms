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
    [SerializeField]
    private UnityEvent<int> onBeatEvent;
    [SerializeField]
    private UnityEvent<string> onMarkerEvent;

    //Note division enum, whole, half, quarter?

    //Storage class for buffering timeline callback event data
    [StructLayout(LayoutKind.Sequential)]
    class TimelineDataBuffer
    {
        //BPM, time, nested events, etc... if needed
        public int currentBeat = 0;
        public int beatBuffer = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
        public UnityEvent<int> onBeatInternal;
        public UnityEvent<string> onMarkerInternal;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            onBeatEvent.Invoke(0);
        }
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT TimelineEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
            return result;
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineDataBuffer timelineInfo = (TimelineDataBuffer)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        //Debug.Log(parameter.tempo);
                        //Debug.Log(parameter.position);
                        //timelineInfo.onBeatInternal.Invoke(timelineInfo.beatBuffer);
                        timelineInfo.beatBuffer++;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        timelineInfo.onMarkerInternal.Invoke((string)parameter.name);
                    }
                    //if we need any other data from the timeline add more cases...
                    //case FMOD.Studio.EVENT_CALLBACK_TYPE.
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }

    public void SetEventInstance(FMOD.Studio.EventInstance instance)
    {
        eventInstance = instance;

        //Instantiate data buffer & copy event hooks
        timelineDataInstance = new TimelineDataBuffer();
        timelineDataInstance.onBeatInternal = onBeatEvent;
        timelineDataInstance.onMarkerInternal = onMarkerEvent;

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