using UnityEngine;

public class InventoryAudioRef : MonoBehaviour
{
    public static InventoryAudioRef instance;
    [SerializeField] private FMOD_Instantiator slotSelect;

    private bool isSetup = false;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }


    public void PlaySlotSelectSound()
    {
        /*
        if (!isSetup)
        {
            isSetup = true;
            return;
        }
        */
        slotSelect.playEvent();
    }
}
