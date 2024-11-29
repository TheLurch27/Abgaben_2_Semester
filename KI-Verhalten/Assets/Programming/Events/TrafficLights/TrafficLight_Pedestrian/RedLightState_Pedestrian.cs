using UnityEngine;

public class RedLightState_Pedestrian : IState
{
    private PedestrianLightController lightController;

    public RedLightState_Pedestrian(PedestrianLightController controller)
    {
        lightController = controller;
    }

    public void Enter()
    {
        // Materialien f�r RedLightState setzen
        lightController.SetLightMaterials(
            lightController.M_TrafficLight_Red_On,
            lightController.M_TrafficLight_Red_On,
            lightController.M_TrafficLight_Green_Off
        );
    }

    public void Execute()
    {
        // Logik w�hrend des roten Lichts (optional)
    }

    public void Exit()
    {
        // Aktionen beim Verlassen des RedLightState (optional)
    }
}
