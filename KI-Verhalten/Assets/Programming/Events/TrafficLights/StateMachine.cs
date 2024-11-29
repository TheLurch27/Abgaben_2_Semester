using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public void SetState(IState newState)
    {
        Debug.Log($"Switching to state: {newState.GetType().Name}");
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Execute()
    {
        currentState?.Execute();
    }

    // Füge diese Methode hinzu
    public IState GetCurrentState()
    {
        return currentState;
    }
}