using UnityEngine;

public class GreenLightState_Pedestrian : IState
{
    private PedestrianLightController lightController;

    public GreenLightState_Pedestrian(PedestrianLightController controller)
    {
        lightController = controller;
    }

    public void Enter()
    {
        // Materialien für GreenLightState setzen
        lightController.SetLightMaterials(
            lightController.M_TrafficLight_Red_Off,
            lightController.M_TrafficLight_Red_Off,
            lightController.M_TrafficLight_Green_On
        );
    }

    public void Execute()
    {
        // Logik während des grünen Lichts (optional)
    }

    public void Exit()
    {
        // Aktionen beim Verlassen des GreenLightState (optional)
    }
}
