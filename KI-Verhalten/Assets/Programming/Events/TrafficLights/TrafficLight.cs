using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private CarLightController carLightController; // Referenz zum CarLightController

    public IState CurrentState { get; private set; }

    private void Start()
    {
        if (carLightController == null)
        {
            // Debug.LogError("CarLightController is not assigned in TrafficLight!");
            return;
        }

        // Initialisiere mit Rot
        SetState(new RedLightState_Car(carLightController));
        Invoke(nameof(SwitchToGreen), 10f); // Nach 10 Sekunden zu Grün wechseln
    }

    private void SwitchToGreen()
    {
        SetState(new GreenLightState_Car(carLightController));
        Invoke(nameof(SwitchToYellow), 10f); // Nach 10 Sekunden zu Gelb wechseln
    }

    private void SwitchToYellow()
    {
        SetState(new YellowLightState_Car(carLightController));
        Invoke(nameof(SwitchToRed), 3f); // Nach 3 Sekunden zu Rot wechseln
    }

    private void SwitchToRed()
    {
        SetState(new RedLightState_Car(carLightController));
        Invoke(nameof(SwitchToGreen), 10f); // Nach 10 Sekunden wieder zu Grün wechseln
    }


    public void SetState(IState newState)
    {
        CurrentState = newState;
        CurrentState.Enter(); // Aktivieren des Zustands

        // Debug.Log($"TrafficLight state set to: {newState.GetType().Name}");
    }
}
