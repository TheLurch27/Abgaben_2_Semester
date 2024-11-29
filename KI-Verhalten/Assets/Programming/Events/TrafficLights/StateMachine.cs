public class StateMachine
{
    private IState currentState;

    public void SetState(IState newState)
    {
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