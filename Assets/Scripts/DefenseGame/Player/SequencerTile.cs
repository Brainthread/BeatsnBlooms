using UnityEngine;

public class SequencerTile : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Renderer borderRenderer;
    [SerializeField] private Renderer innerRenderer;
    private bool isDestroyed;

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
