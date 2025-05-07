using UnityEngine;

/* Music Manager handles the playing of different songs.
 * It is initialized with a Song-object. It then plays the audio clip from said object.
 * 
 */

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    private Song currentSong;
    [SerializeField] private Song testSong;
    private int lastBeat = -1;
    private float pcmLastTime = 0;
    private bool hasLost = false;
    // Test implementation

    //Initialize song
    public void Initialize(Song currentSong)
    {
        instance = this;
        EventHandler.current.onLoss += OnLoss;
        this.currentSong = testSong;
        audioSource = GetComponent<AudioSource>();
        audioSource.resource = currentSong.audio;
        audioSource.Play();
        EventHandler.current.StartSong();
    }
    public float GetSongProgression()
    {
        if(!audioSource||!audioSource.clip||!audioSource.isPlaying)
        {
            return pcmLastTime;
        }
        return (float)audioSource.timeSamples / ((float)audioSource.clip.frequency * (float)audioSource.clip.length);
    }
    public float GetBeat(float bpm, float divider)
    {
        return GetBeatsPerSecond(bpm*divider)*GetSampledTime();
    }
    public float GetBeat(float bpm)
    {
        return GetBeat(bpm, 1);
    }
    public float GetBeat()
    {
        return GetBeat(currentSong.bpm, 1);
    }
    public float GetBeatInterpolationValue()
    {
        return GetBeat(currentSong.bpm) - (float)lastBeat;
    }
    public float GetSecondsUntilNextBeat()
    {
        float bpm = currentSong.bpm;
        return (1-GetBeatInterpolationValue())*(1/GetBeatsPerSecond(bpm));
    }
    public float GetBeatsPerSecond(float bpm)
    {
        return bpm / 60f;
    }
    public float GetBeatsPerSecond()
    {
        return currentSong.bpm / 60f;
    }
    public float GetBPM()
    {
        return currentSong.bpm;
    }
    public float GetSampledTime()
    {
        if(audioSource.isPlaying!=true)
        {
            return pcmLastTime;
        }
        return (float)audioSource.timeSamples / ((float)audioSource.clip.frequency); //check PCM position
    }
    private void NewBeat()
    {
        EventHandler.current.NewBeat();
    }
    private void OnLoss()
    {
        hasLost = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasLost && audioSource.isPlaying==true) {
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, 0, 0.35f*Time.deltaTime);
        }
        if (currentSong != null)
        {
            float pcmTime = GetSampledTime();
            pcmLastTime = pcmTime;
            int currentBeat = Mathf.RoundToInt(Mathf.Floor(GetBeat(currentSong.bpm)));
            if (currentBeat > lastBeat) //Simple comparison to see if we are after the current beat counter
            {
                lastBeat = currentBeat;
                NewBeat();
            }
        }
    }
}


