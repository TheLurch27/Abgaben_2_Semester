using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] private CarLightController carLightController; // Referenz zum CarLightController

    public IState CurrentState { get; private set; }

    private void Start()
    {
        if (carLightController == null)
        {
            Debug.LogError("CarLightController is not assigned in TrafficLight!");
            return;
        }

        // Initialisiere den Startzustand (z. B. RedLightState_Car)
        SetState(new RedLightState_Car(carLightController));
    }

    public void SetState(IState newState)
    {
        CurrentState = newState;
        CurrentState.Enter(); // Rufe den Enter-Methoden-Übergang des Zustands auf
    }
}
