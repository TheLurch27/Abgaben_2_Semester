using UnityEngine;
using System.Collections;

public class CarVision : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f; // Länge des Raycasts
    [SerializeField] private LayerMask layerMask; // Layer, die der Raycast treffen soll

    private RaycastHit hitInfo; // Speichert Informationen über getroffene Objekte
    private CarController carController;

    private void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void Update()
    {
        CheckForward();
    }

    private void CheckForward()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.right);

        if (Physics.Raycast(origin, direction, out hitInfo, rayDistance, layerMask))
        {
            Debug.Log($"Collider hit: {hitInfo.collider.gameObject.name}");

            TrafficLightTrigger trafficLightTrigger = hitInfo.collider.GetComponent<TrafficLightTrigger>();
            if (trafficLightTrigger == null)
            {
                Debug.LogWarning("TrafficLightTrigger not found on collider!");
                return;
            }

            TrafficLight trafficLight = trafficLightTrigger.GetTrafficLight();
            if (trafficLight == null)
            {
                Debug.LogWarning("TrafficLight is not assigned in TrafficLightTrigger!");
                return;
            }

            CheckTrafficLightState(trafficLight);
        }
    }


    private void CheckTrafficLightState(TrafficLight trafficLight)
    {
        if (trafficLight == null)
        {
            Debug.LogWarning("TrafficLight is null! Make sure the TrafficLight is assigned to the TrafficLightTrigger.");
            return;
        }

        var currentState = trafficLight.CurrentState;

        if (currentState == null)
        {
            Debug.LogWarning("CurrentState of TrafficLight is null!");
            return;
        }

        Debug.Log($"Traffic Light State: {currentState.GetType().Name}");

        if (currentState is RedLightState_Car || currentState is YellowLightState_Car)
        {
            Debug.Log("Red or Yellow light detected. Preparing to stop...");
            carController.StopBeforeTrigger(hitInfo.point);
        }
        else if (currentState is GreenLightState_Car)
        {
            Debug.Log("Green light detected. Proceeding...");
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.right);

        Gizmos.DrawLine(origin, origin + direction * rayDistance);

        if (hitInfo.collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(hitInfo.point, 0.2f);
        }
    }
}
