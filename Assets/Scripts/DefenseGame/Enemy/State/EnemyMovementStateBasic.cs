using UnityEngine;

public class EnemyMovementStateBasic : StateMachineState
{
    private Vector2 latestGridPosition = new Vector2(-1337, -1337);
    private Vector2 nextGridPosition;
    [SerializeField] private float GridMovementSpeed = 1;
    [SerializeField] private AnimationCurve movementCurve = new AnimationCurve();
    private int movementQueueIndex = 0;
    [SerializeField] private int[] movementQueue;

    private float movementSpeed = 0;
    bool beating = false;


    [SerializeField] private LayerMask enemyAttackMask;
    [SerializeField] private float meleeRange = 5;
    [SerializeField] private EnemyMeleeAttackState meleeAttackState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        latestGridPosition = GridManager.current.WorldPositionToGridPosition(transform.position);
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
    }


    // Update is called once per frame
    public override void StateUpdate()
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

    public override void OnNewBeat()
    {
        Vector3 pos = transform.position;
        latestGridPosition = nextGridPosition;
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
        movementQueueIndex += 1;
        if (movementQueueIndex >= movementQueue.Length)
            movementQueueIndex = 0;
        if(Physics.Raycast(transform.position, transform.forward, meleeRange, enemyAttackMask))
        {
            stateMachine.SwitchState(meleeAttackState);
        }
    }

    public override void EnterState()
    {
        latestGridPosition = GridManager.current.WorldPositionToGridPosition(transform.position);
        Vector2 movementDirection = new Vector2(transform.forward.x, 0).normalized;
        nextGridPosition = latestGridPosition + movementDirection * movementQueue[movementQueueIndex];
        movementQueueIndex = 0;
    }

    public override void ExitState()
    {
       
    }

    public override void OnStateFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
