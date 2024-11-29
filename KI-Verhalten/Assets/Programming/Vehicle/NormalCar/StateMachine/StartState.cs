using System.Collections;
using System.Collections.Generic;
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
        carController.Accelerate();

        // Wechsel in den DriveState, wenn die maxSpeed erreicht ist
        if (Mathf.Approximately(carController.CurrentSpeed, carController.maxSpeed))
        {
            Debug.Log("Max speed reached. Switching to DriveState.");
            carController.SetState(new DriveState(carController));
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting StartState");
    }
}
