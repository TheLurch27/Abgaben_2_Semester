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
        // Prüfen, ob die Ampel grün ist (basierend auf TrafficLight-Logik)
        if (carController.IsTrafficLightGreen())
        {
            Debug.Log("Green light detected. Switching to StartState.");
            carController.SetState(new StartState(carController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting StopState");
    }
}
