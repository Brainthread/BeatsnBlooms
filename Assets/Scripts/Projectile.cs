using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 latestPosition;
    [SerializeField] private float unitMovementPerBeat = 30;
    private float movementSpeed = 0;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float damage = 1;
    [SerializeField] private float lifeTime = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        latestPosition = transform.position;
        movementSpeed = unitMovementPerBeat / MusicManager.instance.GetBeatsPerSecond();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime<0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        RaycastHit hit;
        Vector3 directionToCurrentPos = (transform.position - latestPosition).normalized;
        float distanceToCurrentPos = (transform.position - latestPosition).magnitude;
        if (Physics.Raycast(latestPosition, directionToCurrentPos, out hit, distanceToCurrentPos, mask))
        {
            if(hit.transform.GetComponent<HealthManager>())
            {
                hit.transform.GetComponent<HealthManager>().ApplyDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
