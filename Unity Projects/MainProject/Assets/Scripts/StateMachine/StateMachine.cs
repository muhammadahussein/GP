using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State currentState;
    protected Coroutine stateRoutine;

    public void SetState(State state)
    {
        currentState?.OnStateExit();
        currentState = state;
        StartCoroutine(currentState.OnStateEnter());
    }
    public void PlayStateRoutine()
    {
        stateRoutine = StartCoroutine(currentState.PlayStateRoutine());
    }
    public void StopStateRoutine()
    {
        StopCoroutine(stateRoutine);
    }
    public void FixedUpdate()
    {
        if (currentState != null)
            StartCoroutine(currentState.OnStateUpdate());
    }
}
