using System.Xml;
using UnityEngine;

public class DriveState : IState
{
    private CarController carController;

    public DriveState(CarController controller)
    {
        carController = controller;
    }

    public void Enter()
    {
        Debug.Log("Entering DriveState: Driving at max speed...");
        carController.CurrentSpeed = carController.maxSpeed; // Geschwindigkeit sofort setzen
    }

    public void Execute()
    {
        // Fahren mit konstanter Geschwindigkeit
    }

    public void Exit()
    {
        Debug.Log("Exiting DriveState");
    }
}