using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager current;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 16;
    
    [SerializeField] private float rowDistance = 12;
    [SerializeField] private float columnDistance = 12;
    [SerializeField] private float rowStartPosition = -63.2f;
    [SerializeField] private float columnStartPosition = -18;

    private void Start()
    {
        current = this;
    }

    public int GetRows()
    {
        return rows;
    }
    public int GetColumns()
    {
        return columns;
    }
    
    public Vector2 ClampedWorldPositionToGridPosition(Vector3 worldPosition, bool x_clamp, bool y_clamp)
    {
        Vector2 gridPosition = WorldPositionToGridPosition(worldPosition);
        if(x_clamp)
        {
            gridPosition.x = Mathf.Clamp(gridPosition.x, 0, columns);
        }
        if(y_clamp)
        {
            gridPosition.y = Mathf.Clamp(gridPosition.y, 0, rows);
        }
        return gridPosition;
    }

    public Vector2 WorldPositionToGridPosition (Vector3 worldPosition)
    {
        float x = (worldPosition.x - rowStartPosition) / columnDistance;
        float y = (worldPosition.z - columnStartPosition) / rowDistance;
        return new Vector2(x, y);
    }

    public Vector3 GridPositionToWorldPosition (Vector2 gridPosition)
    {
        float x = rowStartPosition + gridPosition.x * columnDistance;
        float y = 0;
        float z = columnStartPosition + gridPosition.y * rowDistance;
        return new Vector3(x, y, z);
    }

}
