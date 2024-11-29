using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarVision : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f; // Länge des Raycasts
    [SerializeField] private LayerMask layerMask; // Layer, die der Raycast treffen soll

    private RaycastHit hitInfo; // Speichert Informationen über getroffene Objekte
    private CarController carController;
    private StartState startState;

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
            // Debug.Log($"Collider hit: {hitInfo.collider.gameObject.name}");

            TrafficLightTrigger trafficLightTrigger = hitInfo.collider.GetComponent<TrafficLightTrigger>();
            if (trafficLightTrigger == null)
            {
                // Debug.LogWarning("TrafficLightTrigger not found on collider!");
                return;
            }

            TrafficLight trafficLight = trafficLightTrigger.GetTrafficLight();
            if (trafficLight == null)
            {
                // Debug.LogWarning("TrafficLight is null! Make sure the TrafficLight is assigned in TrafficLightTrigger.");
                return;
            }

            CheckTrafficLightState(trafficLight);
        }
    }


    // private void CheckTrafficLightState(TrafficLight trafficLight)
    // {
    //     var currentState = trafficLight.CurrentState;
    // 
    //     Debug.Log($"Traffic Light State: {currentState.GetType().Name}");
    // 
    //     if (currentState is RedLightState_Car || currentState is YellowLightState_Car)
    //     {
    //         Debug.Log("Red or Yellow light detected. Preparing to stop...");
    //         // Bremsen und 1 Meter vor dem Trigger anhalten
    //         carController.StopBeforeTrigger(hitInfo.point);
    //     }
    //     else if (currentState is GreenLightState_Car)
    //     {
    //         Debug.Log("Green light detected. Proceeding...");
    //         // Auto darf weiterfahren
    //         carController.Accelerate();
    //     }
    // }

    private void CheckTrafficLightState(TrafficLight trafficLight)
    {
        if (trafficLight.CurrentState == null)
        {
            // Debug.LogWarning("TrafficLight state is null!");
            return;
        }

        // Debug.Log($"TrafficLight Current State: {trafficLight.CurrentState.GetType().Name}");

        // Prüfen, ob die Ampel rot oder gelb ist
        if (trafficLight.CurrentState is RedLightState_Car || trafficLight.CurrentState is YellowLightState_Car)
        {
            // Debug.Log("Red or Yellow light detected. Preparing to stop...");
            carController.StopBeforeTrigger(hitInfo.point); // Stop-Logik
        }
        // Prüfen, ob die Ampel grün ist
        else if (trafficLight.CurrentState is GreenLightState_Car)
        {
            // Debug.Log("Green light detected. Proceeding...");
            // Nichts tun oder explizit beschleunigen
            carController.Accelerate();
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
