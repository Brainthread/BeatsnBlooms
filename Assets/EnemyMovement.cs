using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 latestGridPosition = new Vector2(-1337, -1337);
    private Vector2 nextGridPosition;
    [SerializeField] private float GridMovementSpeed = 1;
    [SerializeField] private AnimationCurve movementCurve = new AnimationCurve();
    private int movementQueueIndex = 0;
    [SerializeField] private int[] movementQueue;

    private float movementSpeed = 0;
    bool beating = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventHandler.current.onBeat += OnNewBeat;
        latestGridPosition = GridManager.current.WorldPositionToGridPosition(transform.position);
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
    }


    // Update is called once per frame
    void Update()
    {
            float t = MusicManager.instance.GetBeatInterpolationValue();
            float ev = movementCurve.Evaluate(t);
            Vector2 intermediaryGridPosition = Vector2.Lerp(latestGridPosition, nextGridPosition, ev);
            transform.position = GridManager.current.GridPositionToWorldPosition(intermediaryGridPosition);
    }

    private void OnDestroy()
    {
        EventHandler.current.onBeat -= OnNewBeat;
    }

    void OnNewBeat()
    {
        Vector3 pos = transform.position;
        latestGridPosition = nextGridPosition;
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
        movementQueueIndex += 1;
        if (movementQueueIndex >= movementQueue.Length)
            movementQueueIndex = 0;
    }
}
