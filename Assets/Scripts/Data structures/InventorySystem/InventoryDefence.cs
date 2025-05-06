using UnityEngine;

public class InventoryDefence : MonoBehaviour
{
    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 2;
    [SerializeField] private float rowSpacing = 0.5f;
    [SerializeField] private float columnSpacing = 0.5f;

    [SerializeField] private GameObject inventorySlot;
    public void Activate()
    {
        for(int c = 0; c < columns; c++)
        {
            for(int r = 0; r < rows; r++)
            {

            }
        }
    }
}
