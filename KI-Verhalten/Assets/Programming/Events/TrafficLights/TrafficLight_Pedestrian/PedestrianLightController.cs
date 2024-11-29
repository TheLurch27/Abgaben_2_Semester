using UnityEngine;

public class PedestrianLightController : MonoBehaviour
{
    private StateMachine stateMachine;

    [SerializeField] private Renderer redLightRenderer1; // Erste rote Lampe
    [SerializeField] private Renderer redLightRenderer2; // Zweite rote Lampe
    [SerializeField] private Renderer greenLightRenderer;

    [SerializeField] public Material M_TrafficLight_Red_On;
    [SerializeField] public Material M_TrafficLight_Red_Off;
    [SerializeField] public Material M_TrafficLight_Green_On;
    [SerializeField] public Material M_TrafficLight_Green_Off;

    private void Start()
    {
        stateMachine = new StateMachine();

        // Initialer State ist GreenLightState
        stateMachine.SetState(new GreenLightState_Pedestrian(this));
        Invoke(nameof(SwitchToRed), 8f); // GreenLightState dauert 8 Sekunden
    }

    public void SwitchToRed()
    {
        stateMachine.SetState(new RedLightState_Pedestrian(this));
        Invoke(nameof(SwitchToGreen), 12f); // Fußgänger Rot = 12 Sekunden (inkl. Auto Grün + Gelb)
    }

    public void SwitchToGreen()
    {
        stateMachine.SetState(new GreenLightState_Pedestrian(this));
        Invoke(nameof(SwitchToRed), 8f); // Fußgänger Grün = 8 Sekunden
    }

    public void SetLightMaterials(Material red1, Material red2, Material green)
    {
        redLightRenderer1.material = red1;
        redLightRenderer2.material = red2;
        greenLightRenderer.material = green;
    }
}
