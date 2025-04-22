using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    private Vector3 latestRayPosition;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float damage = 1;
    [SerializeField] private float lifeTime = 3;
    [SerializeField] private float rayRange = 0.3f;

    private Vector2 latestGridPosition = new Vector2(-1337, -1337);
    private Vector2 nextGridPosition;
    [SerializeField] private float GridMovementSpeed = 1;
    [SerializeField] private AnimationCurve movementCurve = new AnimationCurve();
    private int movementQueueIndex = 0;
    [SerializeField] private int[] movementQueue;
    bool beating = false;
    bool detectInRealtime = false;
    [SerializeField] private GameObject hitObject = null;

    void Start()
    {
        latestRayPosition = transform.position;
        EventHandler.current.onBeat += OnNewBeat;
        nextGridPosition = GridManager.current.WorldPositionToGridPosition(transform.position);
        OnNewBeat();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime<0)
        {
            Destroy(gameObject);
        }

        if (beating)
        {
            float t = MusicManager.instance.GetBeatInterpolationValue();
            float ev = movementCurve.Evaluate(t);
            Vector2 intermediaryGridPosition = Vector2.Lerp(latestGridPosition, nextGridPosition, ev);
            transform.position = GridManager.current.GridPositionToWorldPosition(intermediaryGridPosition);
        }
        Debug.DrawLine(latestRayPosition, latestRayPosition + transform.forward*3, Color.red);
        HandleHitDetection();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        EventHandler.current.onBeat -= OnNewBeat;
    }

    void OnNewBeat()
    {
        beating = true;
        Vector3 pos = transform.position;
        latestGridPosition = nextGridPosition;
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        Debug.Log(movementDirection);
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
        movementQueueIndex += 1;
        if (movementQueueIndex >= movementQueue.Length)
            movementQueueIndex = 0;
        HandleHitDetection();
    }

    void HandleHitDetection()
    {
        RaycastHit[] hits;
        if(!hitObject)
        {
            Vector3 directionToCurrentPos = (transform.position - latestRayPosition);
            float distanceToCurrentPos = directionToCurrentPos.magnitude;
            hits = Physics.RaycastAll(latestRayPosition, transform.forward, distanceToCurrentPos + rayRange, mask);
            if (hits.Length > 0)
            {
                Array.Sort(hits, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
                hitObject = hits[0].transform.gameObject;
            
            }
            latestRayPosition = transform.position;
        }
        if (hitObject)
        {
            if (hitObject.transform.GetComponent<HealthManager>())
            {
                hitObject.transform.GetComponent<HealthManager>().ApplyDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
