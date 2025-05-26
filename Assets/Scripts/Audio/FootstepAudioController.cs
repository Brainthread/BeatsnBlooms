using UnityEngine;

public class FootstepAudioController : MonoBehaviour
{
    [SerializeField] private float stepInterval = 0.2f;
    private float timer = 0f;
    private bool walkingActive = false;

    [SerializeField] private FMOD_Instantiator footEvent;
    void Start()
    {
        
    }

    void Update()
    {
        if (!walkingActive)
        {
            timer = 0;
            return;
        }

        if(timer >= stepInterval)
        {
            footEvent.playEvent();
            timer = 0f;
        }
        timer += Time.deltaTime;
    }

    public void SetWalkingState(bool state)
    {
        if (state) StartWalking();
        else StopWalking();
    }

    private void StartWalking()
    {
        if (walkingActive) return;
        walkingActive = true;
        footEvent.playEvent(); //Alternatively leading step sound
    }

    private void StopWalking()
    {
        if (!walkingActive) return;
        walkingActive = false;
        footEvent.playEvent(); //Alternatively trailing step sound
    }
}
