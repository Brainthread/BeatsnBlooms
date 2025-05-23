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

    int stage = 0;
    float beatInterpolation = 0;
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
        if(target != null) {
            targetPosition = target.transform.position;
        }
        if(stage == 0)
        {
            transform.position = Vector3.Lerp(startPosition, startPosition + relativeLoiterPosition, launchCurve.Evaluate(beatInterpolation));
        }
        if (stage == 1)
        {
            
        }
        if (stage == 2)
        {
            transform.position = Vector3.Lerp(startPosition + relativeLoiterPosition, targetPosition, finalApproachCurve.Evaluate(beatInterpolation));
        }
        if (stage == 3)
        {
            Explode();
        }
    }
    public void Explode ()
    {
        FMOD_TimelineCallbacks.instance.OnFrameBeatTime.RemoveListener(OnFrameBeatTime);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void OnBeat()
    {
        stage += 1;
    }
    public void OnFrameBeatTime(float value)
    {
        beatInterpolation = value;
    }
}
