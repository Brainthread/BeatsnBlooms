using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 relativeLoiterPosition = new Vector3(0, 30, 15);
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    private GameObject target = null;
    private Vector3 targetPosition = Vector3.zero;
    [SerializeField] private AnimationCurve launchCurve;
    [SerializeField] private AnimationCurve finalApproachCurve;
    [SerializeField] private float rotationSpeed = 180;
    Vector3 latestBeatPosition = Vector3.zero;

    int stage = 0;
    float beatInterpolation = 0;
    Vector3 currentForward = Vector3.zero;
    Vector3 latestPos = Vector3.zero;
    float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onBeat += OnBeat;
        FMOD_TimelineCallbacks.instance.OnFrameBeatTime.AddListener(OnFrameBeatTime);
        targetPosition = transform.position + transform.forward * 50;
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 200, enemyLayerMask))
        {
            target = hit.transform.gameObject;
        }
        startPosition = transform.position;
        stage = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null) {
            targetPosition = target.transform.position;
        }
        if(stage == 0)
        {
            latestPos = transform.position;
            transform.position = Vector3.Lerp(startPosition, startPosition + relativeLoiterPosition, launchCurve.Evaluate(beatInterpolation));
            Vector3 direction = (startPosition + relativeLoiterPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (stage == 1)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        if (stage == 2)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.Lerp(latestBeatPosition, targetPosition, finalApproachCurve.Evaluate(beatInterpolation));
        }
        if (stage == 3)
        {
            Explode();
        }

    }
    public void Explode ()
    {
        FMOD_TimelineCallbacks.instance.OnFrameBeatTime.RemoveListener(OnFrameBeatTime);
        EventHandler.current.onBeat -= OnBeat;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void OnBeat()
    {
        stage += 1;
        latestBeatPosition = transform.position;
    }
    public void OnFrameBeatTime(float value)
    {
        beatInterpolation = value;
    }
}
