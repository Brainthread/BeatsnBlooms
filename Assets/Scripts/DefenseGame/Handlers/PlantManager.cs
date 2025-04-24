using UnityEngine;

public class PlantManager : MonoBehaviour
{
    [SerializeField] private Plant[] plants;
    private int plantPosition = 4;
    [SerializeField] private float dangerDetectionRange = 90;
    [SerializeField] private float dangerDetectionOffset = -80;
    [SerializeField] private LayerMask dangerDetectionMask;
    private Vector3[] gridPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridPositions = new Vector3[plants.Length];
        EventHandler.current.onPlantActivation += OnPlantActivation;
        for(int i = 0; i < plants.Length; i++)
        {
            if (!plants[i])
                continue;
            gridPositions[i] = GridManager.current.GridPositionToWorldPosition(new Vector2(plantPosition, i));
            plants[i].transform.position = gridPositions[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Handle plants hiding if things get too close
        for(int i = 0; i < gridPositions.Length; i++)
        {
            Vector3 rayOriginPosition = gridPositions[i] + Vector3.right * dangerDetectionOffset;
            Debug.DrawLine(rayOriginPosition, rayOriginPosition + Vector3.right * dangerDetectionRange, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(rayOriginPosition, Vector3.right, out hit, dangerDetectionRange, dangerDetectionMask))
            {
                plants[i].SetActiveStatus(false);
            }
            else
            {
                plants[i].SetActiveStatus(true);
            }
        }
    }

    private void OnPlantActivation(int id, int powerup)
    {
        plants[id].Activate(powerup);
    }
}
