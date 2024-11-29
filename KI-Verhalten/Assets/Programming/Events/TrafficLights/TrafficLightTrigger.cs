using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{
    [SerializeField] private TrafficLight trafficLight; // Referenz zur zugehörigen Ampel

    public TrafficLight GetTrafficLight()
    {
        return trafficLight;
    }
}
