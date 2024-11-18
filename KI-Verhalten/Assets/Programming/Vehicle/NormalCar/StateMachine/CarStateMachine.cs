using UnityEngine;

public class CarStateMachine : MonoBehaviour
{
    private ICarState currentState; // Der aktuelle Zustand des Autos
    private Transform targetPoint; // Das Ziel für das Auto (Point B oder Point D)

    private void Start()
    {
        // Zu Beginn im ApproachState
        SetState(new ApproachState());
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(); // Ruft die Update-Methode des aktuellen Zustands auf
        }
    }

    public void SetState(ICarState newState)
    {
        // Verlasse den aktuellen Zustand
        if (currentState != null)
        {
            currentState.ExitState();
        }

        // Setze den neuen Zustand und rufe EnterState auf
        currentState = newState;
        currentState.EnterState(this);
    }

    // Setzt den Zielpunkt für das Auto
    public void SetTargetPoint(Transform target)
    {
        targetPoint = target;
    }

    // Gibt den Zielpunkt zurück
    public Transform GetTargetPoint()
    {
        return targetPoint;
    }
}
