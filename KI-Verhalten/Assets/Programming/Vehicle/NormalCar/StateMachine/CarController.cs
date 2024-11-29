using UnityEngine;

public class CarController : MonoBehaviour
{
    private StateMachine stateMachine;

    public float maxSpeed = 10f; // Höchstgeschwindigkeit
    public float acceleration = 2f; // Beschleunigungsrate
    public float braking = 3f; // Abbremsrate

    public float CurrentSpeed { get; set; } = 0f; // Aktuelle Geschwindigkeit
    private Transform targetPoint; // Zielpunkt (Despawnpunkt)

    [SerializeField] private LayerMask layerMask;

    private void Start()
    {
        stateMachine = new StateMachine();

        // Initialisiere die State Machine mit dem DriveState
        stateMachine.SetState(new DriveState(this));
    }

    private void Update()
    {
        // Debug.Log($"Current Speed: {CurrentSpeed}");
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
        // Erhöhe die Geschwindigkeit bis zur Höchstgeschwindigkeit
        CurrentSpeed = Mathf.Min(CurrentSpeed + acceleration * Time.deltaTime, maxSpeed);
        transform.Translate(transform.right * CurrentSpeed * Time.deltaTime, Space.World);
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
        // Debug.Log($"Target set to: {targetPoint.position}");
    }

    public void StopBeforeTrigger(Vector3 triggerPoint)
    {
        float stopDistance = 1f; // Abstand vor dem Trigger
        Vector3 stopPosition = triggerPoint - transform.forward * stopDistance;

        // Überprüfen, ob das Auto bereits vor dem Trigger steht
        if (Vector3.Distance(transform.position, stopPosition) > 0.1f)
        {
            // Debug.Log("Slowing down to stop...");
            Brake(); // Langsam bremsen
        }
        else
        {
            //Debug.Log("Car stopped before trigger.");
            Stop(); // Vollständig anhalten
        }
    }

    public bool IsObstacleDetected()
    {
        RaycastHit hit;

        // Raycast in Fahrtrichtung (transform.right, da das Auto auf der Y-Achse gedreht ist)
        if (Physics.Raycast(transform.position, transform.right, out hit, 10f, layerMask))
        {
            // Prüfen, ob der Raycast einen TrafficLightTrigger trifft
            TrafficLightTrigger trigger = hit.collider.GetComponent<TrafficLightTrigger>();
            if (trigger != null)
            {
                // Hole die zugehörige Ampel
                TrafficLight trafficLight = trigger.GetTrafficLight();
                if (trafficLight != null)
                {
                    // Prüfe, ob die Ampel rot oder gelb ist
                    if (trafficLight.CurrentState is RedLightState_Car || trafficLight.CurrentState is YellowLightState_Car)
                    {
                        Debug.Log("Obstacle detected: Red or Yellow light.");
                        return true; // Hindernis erkannt (Auto sollte anhalten)
                    }
                }
            }
        }

        // Kein Hindernis erkannt
        Debug.Log("No obstacle detected.");
        return false;
    }


    public bool IsTrafficLightGreen()
    {
        // Prüfe, ob der Raycast eine Ampel mit grünem Zustand erkannt hat
        TrafficLight trafficLight = GetTrafficLightFromRaycast();

        if (trafficLight != null && trafficLight.CurrentState is GreenLightState_Car)
        {
            return true; // Ampel ist grün
        }

        return false; // Keine grüne Ampel erkannt
    }

    private TrafficLight GetTrafficLightFromRaycast()
    {
        RaycastHit hit;

        // Raycast in Fahrtrichtung (transform.right, da das Auto auf der Y-Achse gedreht ist)
        if (Physics.Raycast(transform.position, transform.right, out hit, 10f, layerMask))
        {
            // Prüfen, ob der getroffene Collider einen TrafficLightTrigger hat
            TrafficLightTrigger trigger = hit.collider.GetComponent<TrafficLightTrigger>();
            if (trigger != null)
            {
                return trigger.GetTrafficLight(); // Gibt die zugehörige TrafficLight-Instanz zurück
            }
        }

        return null; // Keine Ampel erkannt
    }



    private void MoveTowardsTarget()
    {
        // Debug.Log($"Moving towards: {targetPoint.position} with speed: {CurrentSpeed}");
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, CurrentSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
