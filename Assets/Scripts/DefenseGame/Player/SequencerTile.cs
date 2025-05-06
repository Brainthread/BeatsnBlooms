using UnityEngine;
using UnityEngine.Events;

public class SequencerTile : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Renderer borderRenderer;
    [SerializeField] private Renderer innerRenderer;
    private bool isDestroyed;

    [SerializeField] private UnityEvent onTileActivate;
    [SerializeField] private UnityEvent onTileDeactivate;

    public void Start()
    {
        isDestroyed = false;
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
    public void SetInnerMaterial(Material material)
    {
        innerRenderer.material = material;
    }
}
