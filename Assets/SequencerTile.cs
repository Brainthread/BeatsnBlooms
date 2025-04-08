using UnityEngine;

public class SequencerTile : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Renderer borderRenderer;
    [SerializeField] private Renderer innerRenderer;
    
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
