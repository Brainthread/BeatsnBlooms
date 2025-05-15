using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantAction[] plantActions;
    [SerializeField] private bool isActive = true;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float burySpeed = 5;
    [SerializeField] private float unburrowSpeed = 10;
    private PlantManager plantManager;
    private int row = -1;

    private PlantActionHandler plantActionHandler;
    public int Row
    {
        get { return row; }
        set { row = value; }
    }
    private float burrowDepth = 5.5f;
    public void Start()
    {
        plantManager = PlantManager.current;
        startPosition = transform.position;
        plantActionHandler = GetComponentInChildren<PlantActionHandler>();
    }

    public void SetActiveStatus(bool isActive)
    {
        this.isActive = isActive;
    }

    public void Activate(TileAction.TileActionTypes actionType)
    {
        if(isActive)
        {
            print("PLANT ACTIVATED");
            //plantActions[0].Activate();
            plantActionHandler.GetAction(actionType).Activate(this);
        }
    }
    public void Update()
    {
        transform.position = isActive ?
            Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * unburrowSpeed) :
            Vector3.MoveTowards(transform.position, startPosition - Vector3.up * burrowDepth, Time.deltaTime * burySpeed);       
    }

    public void OnDestroy()
    {
        plantManager.OnDestroyPlant(row);
    }
}

public abstract class PlantAction : MonoBehaviour
{
    [SerializeField] private Texture2D icon; 
    public abstract void Activate(Plant plant);
    public abstract Texture2D GetIcon();
}