using UnityEngine;
using UnityEngine.Events;

public class SequencerTile : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Renderer borderRenderer;
    [SerializeField] private Renderer innerRenderer;
    [SerializeField] private Sequencer sequencer;
    public Sequencer Sequencer { set { sequencer = value; } }
    private bool isDestroyed;

    [SerializeField] private UnityEvent onTileActivate;
    [SerializeField] private UnityEvent onTileDeactivate;

    [SerializeField] private TileAction.TileActionTypes currentAction = TileAction.TileActionTypes.ATTACK;

    //Audio
    [SerializeField] private string MusicParamName;

    public void Start()
    {
        isDestroyed = false;
        GetComponent<HealthManager>().onHealthDepleted += OnHealthDepleted;
    }
    public void OnHealthDepleted()
    {
        sequencer.TileDestroyed(id);
    }
    public bool GetDestructionStatus()
    {
        return isDestroyed;
    }
    public void SetID(int id)
    {
        this.id = id;
    }
    public void SetMusicParamName(string name)
    {
        MusicParamName = name;
    }

    public string GetMusicParam()
    {
        return MusicParamName;
    }
    public void ClickedTile()
    {
        EventHandler.current.ClickSequencerTile(id);
        onTileActivate.Invoke();
        //FMOD_TimelineCallbacks.instance.GetMusicEvent().setParam(MusicParamName, 1);
    }
    public void UnClickedTile()
    {
        EventHandler.current.UnClickSequencerTile(id);
        onTileDeactivate.Invoke();
        Debug.Log("unclik");
        //FMOD_TimelineCallbacks.instance.GetMusicEvent().setParam(MusicParamName, 0);
    }
    public void SetBorderMaterial(Material material)
    {
        borderRenderer.material = material;
    }

    
    public void SetInnerMaterial(Material material)
    {
        innerRenderer.material = material;
    }

    //Inventory Logic Functions
    public void SetPlantAction(TileAction.TileActionTypes actionType)
    {
        currentAction = actionType;
        //Debug.Log("Set Tile Action: " + actionType);
    }
    public TileAction.TileActionTypes GetAndConsumePlantAction()
    {
        //Consume the action from inventory & set tile back to default attack
        //Get the slot by type since there is no guarantee that what's being
        //consumed matches the current selected inventory slot
        DefenceInventorySlot slot = InventoryManager.instance.inventoryDefence.GetInventorySlotByType(currentAction);
        if (slot != null) slot.Consume(); //"Attack" is a hard coded default action. If we don't have a slot for it we get null here
        TileAction.TileActionTypes typeBuffer = currentAction;
        currentAction = TileAction.TileActionTypes.ATTACK;

        return typeBuffer;
    }

    public TileAction.TileActionTypes GetPlantAction()
    {
        return currentAction;
    }
}
