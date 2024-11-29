using UnityEngine;

public class CarController : MonoBehaviour
{
    private StateMachine stateMachine;

    public float maxSpeed = 10f; // Höchstgeschwindigkeit
    public float acceleration = 2f; // Beschleunigungsrate
    public float braking = 3f; // Abbremsrate

    public float CurrentSpeed { get; set; } = 0f; // Aktuelle Geschwindigkeit
    private Transform targetPoint; // Zielpunkt (Despawnpunkt)

    private void Start()
    {
        stateMachine = new StateMachine();

        // Initialisiere die State Machine mit dem DriveState
        stateMachine.SetState(new DriveState(this));
    }

    private void Update()
    {
        Debug.Log($"Current Speed: {CurrentSpeed}");
        stateMachine.Execute();

        if (targetPoint != null && !(stateMachine.GetCurrentState() is StopState))
        {
            MoveTowardsTarget();
        }
    }

    public void SetState(IState newState)
    {
        stateMachine.SetState(newState);
    }

    public void Accelerate()
    {
        CurrentSpeed = Mathf.Min(CurrentSpeed + acceleration * Time.deltaTime, maxSpeed);
        Debug.Log($"Accelerating: Current Speed = {CurrentSpeed}");
    }

    public void Brake()
    {
        // Verringere die Geschwindigkeit bis zum Stillstand
        CurrentSpeed = Mathf.Max(CurrentSpeed - braking * Time.deltaTime, 0f);
    }

    public void Stop()
    {
        // Setze die Geschwindigkeit auf 0
        CurrentSpeed = 0f;
    }

    public bool IsStopped()
    {
        // Überprüfe, ob das Auto steht
        return CurrentSpeed <= 0f;
    }

    public void SetTarget(Transform target)
    {
        targetPoint = target;
        Debug.Log($"Target set to: {targetPoint.position}");
    }

    public void StopBeforeTrigger(Vector3 triggerPoint)
    {
        float stopDistance = 1f; // Abstand vor dem Trigger
        Vector3 stopPosition = triggerPoint - transform.forward * stopDistance;

        // Überprüfen, ob das Auto bereits vor dem Trigger steht
        if (Vector3.Distance(transform.position, stopPosition) > 0.1f)
        {
            Debug.Log("Slowing down to stop...");
            Brake(); // Langsam bremsen
        }
        else
        {
            Debug.Log("Car stopped before trigger.");
            Stop(); // Vollständig anhalten
        }
    }


    private void MoveTowardsTarget()
    {
        Debug.Log($"Moving towards: {targetPoint.position} with speed: {CurrentSpeed}");
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, CurrentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
