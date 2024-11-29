using UnityEngine;

public class YellowLightState_Car : IState
{
    private CarLightController lightController;

    public YellowLightState_Car(CarLightController controller)
    {
        lightController = controller;
    }

    public void Enter()
    {
        lightController.SetLightMaterials(
            lightController.M_TrafficLight_Red_On,
            lightController.M_TrafficLight_Yellow_On,
            lightController.M_TrafficLight_Green_Off
        );
    }

    public void Execute()
    {
        // Keine spezifische Logik während des gelben Lichts
    }

    public void Exit()
    {
        // Keine Aktion beim Verlassen
    }
}
