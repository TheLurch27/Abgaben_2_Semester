using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{
    [SerializeField] private TrafficLight trafficLight; // Referenz zur zugeh�rigen Ampel

    public TrafficLight GetTrafficLight()
    {
        return trafficLight;
    }
}
