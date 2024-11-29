using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{
    [SerializeField] private TrafficLight trafficLight; // Referenz zur zugehörigen Ampel

    public TrafficLight GetTrafficLight()
    {
        if (trafficLight == null)
        {
            // Debug.LogWarning("TrafficLight is not assigned to the TrafficLightTrigger!");
        }
        return trafficLight;
    }
}
