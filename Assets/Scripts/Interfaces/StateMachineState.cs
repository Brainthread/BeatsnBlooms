using UnityEngine;

public abstract class StateMachineState : MonoBehaviour
{
    [SerializeField] protected StateMachine stateMachine;
    public virtual void Initialize(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void StateUpdate();
    public abstract void OnStateFixedUpdate();
    public abstract void OnNewBeat();
}
