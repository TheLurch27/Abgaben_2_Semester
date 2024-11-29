using UnityEngine;

public class CarLightController : MonoBehaviour
{
    private StateMachine stateMachine;

    [SerializeField] private Renderer redLightRenderer;
    [SerializeField] private Renderer yellowLightRenderer;
    [SerializeField] private Renderer greenLightRenderer;

    [SerializeField] public Material M_TrafficLight_Red_On;
    [SerializeField] public Material M_TrafficLight_Red_Off;
    [SerializeField] public Material M_TrafficLight_Yellow_On;
    [SerializeField] public Material M_TrafficLight_Yellow_Off;
    [SerializeField] public Material M_TrafficLight_Green_On;
    [SerializeField] public Material M_TrafficLight_Green_Off;


    private void Start()
    {
        stateMachine = new StateMachine();

        // Initialer State ist RedLightState
        stateMachine.SetState(new RedLightState_Car(this));
        Invoke(nameof(SwitchToYellow), 7f); // RedLightState dauert 7 Sekunden
    }

    public void SwitchToRed()
    {
        stateMachine.SetState(new RedLightState_Car(this));
        Invoke(nameof(SwitchToYellow), 12f); // Auto Rot = 12 Sekunden (inkl. Fußgänger Grün und Puffer)
    }

    public void SwitchToYellow()
    {
        stateMachine.SetState(new YellowLightState_Car(this));
        Invoke(nameof(SwitchToGreen), 3f); // Auto Gelb = 3 Sekunden
    }

    public void SwitchToGreen()
    {
        stateMachine.SetState(new GreenLightState_Car(this));
        Invoke(nameof(SwitchToRed), 10f); // Auto Grün = 10 Sekunden
    }

    public void SetLightMaterials(Material red, Material yellow, Material green)
    {
        redLightRenderer.material = red;
        yellowLightRenderer.material = yellow;
        greenLightRenderer.material = green;
    }
}
