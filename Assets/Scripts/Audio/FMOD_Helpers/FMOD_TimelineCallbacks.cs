using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

class ScriptUsageTimeline : MonoBehaviour
{
    public UnityEvent myBeat;

    //Storage class for buffering timeline callback event data
    [StructLayout(LayoutKind.Sequential)]
    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
        public UnityEvent testEvent;
    }

    TimelineInfo timelineInfo;
    
    GCHandle timelineHandle;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance; //Switch to instantiator

    void Start()
    {
        timelineInfo = new TimelineInfo();
        timelineInfo.testEvent = myBeat;

        //Instantiate our timeline callback
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        //Event Instance
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/TestTimeline");

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        //Add the callback to the event
        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        
        //Start the event
        musicInstance.start();
    }

    void OnDestroy()
    {
        //Garbage Collection
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    void OnGUI()
    {
        //GUILayout.Box(String.Format("Current Bar = {0}, Last Marker = {1}", timelineInfo.currentMusicBar, (string)timelineInfo.lastMarker));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                        //Debug.Log(parameter.position);
                        //Debug.Log(parameter.beat);
                        timelineInfo.testEvent.Invoke();
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        Debug.Log(parameter.name);
                    }
                    //case FMOD.Studio.EVENT_CALLBACK_TYPE.
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }
}