using UnityEngine;

public class DriveState : ICarState
{
    private float speed = 10f;
    private CarStateMachine carStateMachine;

    public void EnterState(CarStateMachine car)
    {
        carStateMachine = car;
        Debug.Log("Entering DriveState");
    }

    public void ExitState()
    {
        Debug.Log("Exiting DriveState");
    }

    public void UpdateState()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        // Zugriff auf das Auto über die CarStateMachine
        carStateMachine.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
