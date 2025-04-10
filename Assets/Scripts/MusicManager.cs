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

    private float pcmDeltaTime;
    private float pcmLastTime = 0;
    // Test implementation
    private void Start()
    {
        instance = this;
        Invoke("Initialize", 0.5f);
    }

    //Initialize song
    private void Initialize()
    {
        this.currentSong = testSong;
        audioSource = GetComponent<AudioSource>();
        audioSource.resource = currentSong.audio;
        audioSource.Play();
        EventHandler.current.StartSong();
    }
    public float GetBeat(float bpm)
    {
        return GetBeatsPerSecond(bpm)*GetSampledTime();
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
            return 0.000001f;
        }
        return (float)audioSource.timeSamples / ((float)audioSource.clip.frequency); //check PCM position
    }
    private void NewBeat()
    {
        EventHandler.current.NewBeat();
    }
    private float GetPCMDeltaTime()
    {
        return pcmDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Initialize();
        }
        if (currentSong != null)
        {
            float pcmTime = GetSampledTime();
            pcmDeltaTime = pcmLastTime-pcmTime;
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


