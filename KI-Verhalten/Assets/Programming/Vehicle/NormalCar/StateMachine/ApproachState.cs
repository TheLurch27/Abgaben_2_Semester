using UnityEngine;

public class ApproachState : ICarState
{
    private float speed = 0f;
    private float maxSpeed = 10f;
    private float acceleration = 1f;
    private CarStateMachine carStateMachine;

    public void EnterState(CarStateMachine car)
    {
        carStateMachine = car;
        Debug.Log("Entering ApproachState");
    }

    public void ExitState()
    {
        Debug.Log("Exiting ApproachState");
    }

    public void UpdateState()
    {
        speed += acceleration * Time.deltaTime;

        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
            carStateMachine.SetState(new DriveState()); // Wechsel zu DriveState
        }

        MoveCar();
    }

    private void MoveCar()
    {
        // Zugriff auf das Auto über die CarStateMachine
        carStateMachine.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
