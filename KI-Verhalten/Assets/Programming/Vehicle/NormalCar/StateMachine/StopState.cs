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
        // Das Auto bleibt stehen, bis eine neue Bedingung erf�llt ist (z. B. gr�ne Ampel)
    }

    public void Exit()
    {
        Debug.Log("Exiting StopState");
    }
}
