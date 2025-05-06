using UnityEngine;

public class TestItem : MonoBehaviour, ITile
{
    public TILE_TYPE Type { get; set; } = TILE_TYPE.BARRIER;
    public int StackSize { get; set; } = 1;
    public bool IsSelected { get; set; }
    void Start()
    {

    }

    void Update()
    {
        
    }

    
    public void PlaceTile(/*seq slot*/)
    {
        //Place ability on tile > find plant > set plant action for that step...
    }
}
