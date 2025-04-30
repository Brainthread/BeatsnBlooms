using UnityEngine;

public class CameraDangerPan : MonoBehaviour
{
    [SerializeField] private bool inDanger = false;
    private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float panSpeed = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        inDanger = false;
        EventHandler.current.onGeneratorDanger += OnGeneratorDanger;
    }

    // Update is called once per frame
    void Update()
    {
        if(inDanger)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, Time.deltaTime*panSpeed);
        }   
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, Time.deltaTime * panSpeed);
        }
    }

    public void OnGeneratorDanger(bool value)
    {
        inDanger = value;
    }
}
