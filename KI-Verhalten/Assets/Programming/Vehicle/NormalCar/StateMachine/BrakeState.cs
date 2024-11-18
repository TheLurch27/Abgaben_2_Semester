using UnityEngine;

public class BrakeState : ICarState
{
    private float speed = 10f;
    private float deceleration = 2f;
    private CarStateMachine carStateMachine;

    public void EnterState(CarStateMachine car)
    {
        carStateMachine = car;
        Debug.Log("Entering BrakeState");
    }

    public void ExitState()
    {
        Debug.Log("Exiting BrakeState");
    }

    public void UpdateState()
    {
        speed -= deceleration * Time.deltaTime;

        if (speed <= 0)
        {
            speed = 0;
            carStateMachine.SetState(new StopState()); // Wechsel zu StopState
        }

        MoveCar();
    }

    private void MoveCar()
    {
        // Zugriff auf das Auto über die CarStateMachine
        carStateMachine.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
