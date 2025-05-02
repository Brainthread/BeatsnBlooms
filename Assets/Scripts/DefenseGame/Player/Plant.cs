using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] private PlantAction[] plantActions;
    [SerializeField] private bool isActive = true;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float burySpeed = 5;
    [SerializeField] private float unburrowSpeed = 10;
    private float burrowDepth = 5.5f;
    public void Start()
    {
        startPosition = transform.position;
    }

    public void SetActiveStatus(bool isActive)
    {
        this.isActive = isActive;
    }

    public void Activate(int ability)
    {
        if(isActive)
        {
            plantActions[ability].Activate();
        }
    }

    public void Update()
    {
        transform.position = isActive ?
            Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * unburrowSpeed) :
            Vector3.MoveTowards(transform.position, startPosition - Vector3.up * burrowDepth, Time.deltaTime * burySpeed);
            
    }


}

public abstract class PlantAction : MonoBehaviour
{
    [SerializeField] private Texture2D icon; 
    public abstract void Activate();
    public abstract Texture2D GetIcon();
}