using UnityEngine;

public class StartState : IState
{
    private CarController carController;

    public StartState(CarController controller)
    {
        carController = controller;
    }

    public void Enter()
    {
        Debug.Log("Entering StartState: Accelerating...");
        carController.CurrentSpeed = 0; // Geschwindigkeit auf 0 setzen
    }

    public void Execute()
    {
        carController.Accelerate(); // Geschwindigkeit erhöhen

        if (Mathf.Approximately(carController.CurrentSpeed, carController.maxSpeed))
        {
            carController.SetState(new DriveState(carController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting StartState");
    }
}
