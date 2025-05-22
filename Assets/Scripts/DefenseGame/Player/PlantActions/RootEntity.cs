using UnityEngine;
public class RootEntity : MonoBehaviour
{
    [SerializeField] private int existTime = 4;
    private GameObject hostObject;
    [SerializeField] private float yOffset = -15;
    [SerializeField] private float surfaceSpeed = 40;
    private Vector3 targetPosition = Vector3.zero;

    private void Start()
    {
        targetPosition = transform.position;
        Vector3 newPos = targetPosition;
        newPos.y += yOffset;
        transform.position= newPos;
        EventHandler.current.onBeat += OnBeat;
    }
    private void OnBeat()
    {
        existTime -= 1;
        if(existTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * surfaceSpeed);
        }
        if(hostObject == null)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        EventHandler.current.onBeat -= OnBeat;
        if(hostObject != null)
        {
            hostObject.GetComponent<EnemyMovementStateBasic>().enabled = true;
            hostObject.GetComponent<EnemyMeleeAttackState>().enabled = true;
        }
    }

    internal void SetHost(GameObject gameObject)
    {
        hostObject = gameObject;
        print(hostObject.name);
        if (hostObject != null)
        {
            hostObject.GetComponent<EnemyMovementStateBasic>().enabled = false;
            hostObject.GetComponent<EnemyMeleeAttackState>().enabled = false;
            hostObject.GetComponent<EnemyMovementStateBasic>().SetPositioning();
        }
    }
}