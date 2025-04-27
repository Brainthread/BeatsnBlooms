using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateMachineState[] states;
    [SerializeField] private StateMachineState current_state;

    void Start()
    {
        EventHandler.current.onBeat += OnNewBeat;
        for (int i = 0; i < states.Length; i++)
        {
            states[i].Initialize(this);
        }
        current_state.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        current_state.StateUpdate();
    }

    public void SwitchState(StateMachineState state)
    {
        current_state.ExitState();
        state.EnterState();
        current_state = state;
    }
    public void OnNewBeat()
    {
        current_state.OnNewBeat();
    }
    public void OnDestroy()
    {
        EventHandler.current.onBeat -= OnNewBeat;
    }
}
