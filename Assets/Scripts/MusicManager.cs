using UnityEngine;

/* Music Manager handles the playing of different songs.
 * It is initialized with a Song-object. It then plays the audio clip from said object.
 * 
 */

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    private Song currentSong;
    [SerializeField] private Song testSong;
    private int lastBeat = -1;
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
    }
    public float GetBeat(float bpm)
    {
        return (float)bpm /(60f)*GetSampledTime();
    }
    public float GetBPM()
    {
        return currentSong.bpm;
    }
    public float GetSampledTime()
    {
        return (float)audioSource.timeSamples / ((float)audioSource.clip.frequency); //check PCM position
    }

    private void NewBeat()
    {
        EventHandler.current.NewBeat();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSong != null)
        {
            int currentBeat = Mathf.RoundToInt(Mathf.Floor(GetBeat(currentSong.bpm)));
            if (currentBeat > lastBeat) //Simple comparison to see if we are after the current beat counter
            {
                lastBeat = currentBeat;
                audioSource2.Play();
                NewBeat();
            }
        }
    }
}


