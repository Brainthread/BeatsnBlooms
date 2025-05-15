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
    public void ClickedTile()
    {
        EventHandler.current.ClickSequencerTile(id);
        onTileActivate.Invoke();
    }
    public void UnClickedTile()
    {
        EventHandler.current.UnClickSequencerTile(id);
        onTileDeactivate.Invoke();
    }
    public void SetBorderMaterial(Material material)
    {
        borderRenderer.material = material;
    }

    //Inventory Logic Functions
    public void SetInnerMaterial(Material material)
    {
        innerRenderer.material = material;
    }
    public void SetPlantAction(TileAction.TileActionTypes actionType)
    {
        currentAction = actionType;
        Debug.Log("Set Tile Action: " + actionType);
    }
    public TileAction.TileActionTypes GetPlantAction()
    {
        //Here we can consume the action from inventory
        //& set tile back to base attack
        return currentAction;
    }
}
