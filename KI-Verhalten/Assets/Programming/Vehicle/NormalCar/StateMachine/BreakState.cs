using UnityEngine;

public class BrakeState : IState
{
    private CarController carController;
    private Transform hitTransform;

    public BrakeState(CarController controller)
    {
        carController = controller;
    }

    public void Enter()
    {
        Debug.Log("Entering BrakeState: Slowing down...");
    }

    public void Execute()
    {
        // Bremsen
        carController.Brake();

        // Überprüfen, ob das Auto fast steht (kleine Geschwindigkeit)
        if (carController.CurrentSpeed <= 0.1f)
        {
            Debug.Log("Switching to StopState...");
            carController.SetState(new StopState(carController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting BrakeState");
    }
}
