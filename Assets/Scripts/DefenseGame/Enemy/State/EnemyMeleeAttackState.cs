using UnityEngine;

public class EnemyMeleeAttackState : StateMachineState
{
    [SerializeField] private float meleeDamage = 1f;
    [SerializeField] private bool[] attackQueue;
    [SerializeField] private int attackQueueIndex;
    [SerializeField] private float meleeRange = 5f;
    [SerializeField] private LayerMask meleeMask;
    [SerializeField] private EnemyMovementStateBasic movementState;
    [SerializeField] private GameObject target;
    private Vector3 targetPosition;
    private Vector3 originalPosition;
    [SerializeField] private float targetApproachSpeed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
        attackQueueIndex = 0;

    }


    // Update is called once per frame
    public override void StateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, targetApproachSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPosition)<0.5f)
        {
            targetPosition = originalPosition;
            targetApproachSpeed = 50f;
        }
    }

    private void OnDestroy()
    {
        EventHandler.current.onBeat -= OnNewBeat;
    }

    public override void OnNewBeat()
    {
        if(target == null)
        {
            stateMachine.SwitchState(movementState);
        }
        if (attackQueue[attackQueueIndex])
        {
            Attack();
        }
    }

    public void DetectTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(originalPosition, transform.forward, out hit, meleeRange, meleeMask))
        {
            target = hit.transform.gameObject;
        }
        else
        {
            target = null;
            return;
        }
    }

    public void Attack()
    {
        transform.position = originalPosition;
        DetectTarget();
        if(target == null)
            return; 
        targetPosition = target.transform.position;
        print("TPos"+targetPosition);
        print("OPos" + originalPosition);
        if (target.GetComponent<HealthManager>())
            target.GetComponent<HealthManager>().ApplyDamage(meleeDamage);
        print("attacked!");
        targetApproachSpeed = 100f;
    }

    public override void EnterState()
    {
        print("EnterState" + Time.time);
        attackQueueIndex = 0;
        originalPosition = transform.position;
        targetPosition = transform.position;
        DetectTarget();
    }

    public override void ExitState()
    {

    }

    public override void OnStateFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
