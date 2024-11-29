using UnityEngine;

public class StopState : IState
{
    private CarController carController;

    public StopState(CarController controller)
    {
        carController = controller;
    }

    public void Enter()
    {
        carController.Stop();
        Debug.Log("Entering StopState: Car stopped.");
    }

    public void Execute()
    {
        // Das Auto bleibt stehen, bis eine neue Bedingung erfüllt ist (z. B. grüne Ampel)
    }

    public void Exit()
    {
        Debug.Log("Exiting StopState");
    }
}
