using UnityEngine;

public class StopState : ICarState
{
    public void EnterState(CarStateMachine car)
    {
        Debug.Log("Entering StopState");
    }

    public void ExitState()
    {
        Debug.Log("Exiting StopState");
    }

    public void UpdateState()
    {
        // Das Auto bleibt stehen, bis es wieder losfahren möchte
    }
}
