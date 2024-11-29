using UnityEngine;

public class GreenLightState_Car : IState
{
    private CarLightController lightController;

    public GreenLightState_Car(CarLightController controller)
    {
        lightController = controller;
    }

    public void Enter()
    {
        lightController.SetLightMaterials(
            lightController.M_TrafficLight_Red_Off,
            lightController.M_TrafficLight_Yellow_Off,
            lightController.M_TrafficLight_Green_On
        );
    }

    public void Execute()
    {
        // Keine spezifische Logik während des grünen Lichts
    }

    public void Exit()
    {
        // Keine Aktion beim Verlassen
    }
}
