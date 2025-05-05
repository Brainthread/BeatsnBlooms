using UnityEngine;

public class SequencerTile : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Renderer borderRenderer;
    [SerializeField] private Renderer innerRenderer;
    [SerializeField] private Sequencer sequencer;
    public Sequencer Sequencer { set { sequencer = value; } }
    private bool isDestroyed;

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
    }
    public void UnClickedTile()
    {
        EventHandler.current.UnClickSequencerTile(id);
    }
    public void SetBorderMaterial(Material material)
    {
        borderRenderer.material = material;
    }
    public void SetInnerMaterial(Material material)
    {
        innerRenderer.material = material;
    }
}
